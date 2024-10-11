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
            ViewData["ActivePage"] = "Lista de Médicos";
            var Query = await climedico.GetAll();
            var listDto = Query.Select(item =>
            {
                var dto = item.ToDto();
                dto.RutFormateado = GetFunctions.FormatearRut(dto.Rut, dto.Dv);
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

            ViewBag.Estado = DropDownList.Estado;

            var Query = await climedico.GetById(id);

            if (Query == null) { return NotFound(); }

            return View(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoCliMedico dto)
        {
            if (id != dto.IdMedico) { return NotFound(); }

            climedico.Update(dto.ToOriginal());
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