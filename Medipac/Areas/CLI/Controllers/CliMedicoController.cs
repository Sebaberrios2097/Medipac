using Humanizer;
using Medipac.Areas.CLI.Data.DTO;
using Medipac.Areas.CLI.Data.Interfaces;
using Medipac.Areas.COM.Data.Interfaces;
using Medipac.Areas.RES.Data.Interfaces;
using Medipac.Models;
using Medipac.ReadOnly;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Areas.CLI.Controllers
{
    [Area("CLI")]
    public class CliMedicoController : Controller
    {
        private readonly ICliMedicoRepository climedico;
        private readonly IComUsuarioRepository comUsuario;
        private readonly IResEspecialidadesRepository resEspecialidades;
        private readonly IResMedicoEspecialidadRepository resMedicoEspecialidad;

        public CliMedicoController(ICliMedicoRepository climedico,
                                   IComUsuarioRepository comUsuario,
                                   IResEspecialidadesRepository resEspecialidades,
                                   IResMedicoEspecialidadRepository resMedicoEspecialidad)
        {
            this.climedico = climedico;
            this.comUsuario = comUsuario;
            this.resEspecialidades = resEspecialidades;
            this.resMedicoEspecialidad = resMedicoEspecialidad;
        }

        public async Task<ActionResult> Index()
        {
            ViewData["ActivePage"] = "Lista de Medicos";
            var Query = await climedico.GetAll();
            var listDto = Query.Select(item =>
            {
                var dto = item.ToDto();
                dto.RutFormateado = GetFunctions.FormatearRut(dto.Rut, dto.Dv);
                dto.NombresEspecialidades = item.ResMedicoEspecialidad.Select(e => e.IdEspecialidadNavigation.Nombre).ToList();


                return dto;
            }).ToList();

            return PartialView(listDto);
        }


        public async Task<ActionResult> Details(int id)
        {
            var Query = await climedico.GetById(id);
            return View(Query.ToDto());
        }

        public async Task<ActionResult> Create()
        {
            ViewData["ActivePage"] = "Lista de Medicos";
            // Cargar los estados (asegurarse de que DropDownList.Estado esté bien definido)
            ViewBag.Estado = DropDownList.Estado;

            // Cargar las especialidades disponibles
            var especialidades = await resEspecialidades.GetAll(); // Asegúrate de tener un método GetAll() en tu repositorio de especialidades.
            ViewBag.Especialidades = especialidades.Select(e => new { e.IdEspecialidad, e.Nombre }).ToList();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoCliMedico Dto)
        {
            ComUsuario newUsuario = new()
            {
                Usuario = $"{Dto.Nombres[..3].ToLower()}.{Dto.ApPaterno.ToLower()}",
                Password = Dto.Rut.ToString(),
                FechaCreacion = DateTime.Now,
                IdUsuario = Dto.IdUsuario,
                IdEstado = 2 // Estado 'Activo' por defecto.

            };

            // Guardar usuario
            var guardarUsuario = await comUsuario.Add(newUsuario);
            var ResultGuardarUsuario = await comUsuario.Save();

            // Obtener Id del usuario creado y dárselo al médico
            var usuarioMedico = await comUsuario.GetById(newUsuario.IdUsuario);
            Dto.IdUsuario = usuarioMedico.IdUsuario;

            // Generar digito verificador del rut y almacenarlo
            Dto.Dv = GetFunctions.CalcularDvRut(Dto.Rut);

            var Query = await climedico.Add(Dto.ToOriginal());
            var Result = await climedico.Save();

            // Guardar especialidades seleccionadas
            if (Result > 0 && Dto.EspecialidadesSeleccionadas != null && Dto.EspecialidadesSeleccionadas.Any())
            {
                foreach (var especialidadId in Dto.EspecialidadesSeleccionadas)
                {
                    var medicoEspecialidad = new ResMedicoEspecialidad
                    {
                        IdMedico = Query.IdMedico,
                        IdEspecialidad = especialidadId
                    };
                    await resMedicoEspecialidad.Add(medicoEspecialidad);
                }
                await resMedicoEspecialidad.Save();
            }

            if (Result > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(Query.ToDto());
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            ViewData["ActivePage"] = "Lista de Medicos";
            ViewBag.Estado = DropDownList.Estado;

            // Obtener el médico con sus especialidades
            var Query = await climedico.GetById(id);

            if (Query == null)
            {
                return NotFound();
            }

            // Cargar todas las especialidades disponibles para el dropdown
            var especialidades = await resEspecialidades.GetAll();
            ViewBag.Especialidades = especialidades
                .Select(e => new { e.IdEspecialidad, e.Nombre })
                .ToList();

            // Obtener los IDs y nombres de las especialidades seleccionadas
            var espSelIds = Query.ResMedicoEspecialidad
                                 .Select(m => m.IdEspecialidad)
                                 .ToList();

            var espSelNombres = Query.ResMedicoEspecialidad
                                     .Select(m => m.IdEspecialidadNavigation.Nombre)
                                     .ToList();

            // Mapear al DTO y asignar las especialidades seleccionadas
            var queryDto = Query.ToDto();
            queryDto.EspecialidadesSeleccionadas = espSelIds;  // Asignar los IDs
            queryDto.NombresEspecialidades = espSelNombres;    // Asignar los nombres

            return View(queryDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoCliMedico dto)
        {
            if (id != dto.IdMedico)
            {
                return NotFound();
            }

            // Generar dígito verificador del RUT y almacenarlo
            dto.Dv = GetFunctions.CalcularDvRut(dto.Rut);

            // Obtener el médico con sus relaciones actuales de especialidades
            var medico = await climedico.GetById(id);

            if (medico == null)
            {
                return NotFound();
            }

            // Eliminar las especialidades actuales del médico
            medico.ResMedicoEspecialidad.Clear();

            // Agregar las nuevas especialidades seleccionadas
            foreach (var especialidadId in dto.EspecialidadesSeleccionadas)
            {
                medico.ResMedicoEspecialidad.Add(new ResMedicoEspecialidad
                {
                    IdMedico = medico.IdMedico,
                    IdEspecialidad = especialidadId
                });
            }

            // Actualizar los datos del médico (sin perder las relaciones de especialidades)
            climedico.Update(dto.ToOriginal());

            // Guardar los cambios en la base de datos
            _ = await climedico.Save();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await climedico.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Query = await climedico.DeleteById(id);
            var Result = await climedico.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}