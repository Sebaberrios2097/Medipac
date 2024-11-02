using Medipac.Areas.CLI.Data.DTO;
using Medipac.Areas.CLI.Data.Interfaces;
using Medipac.Areas.COM.Data.Interfaces;
using Medipac.Areas.RES.Data.Interfaces;
using Medipac.Models;
using Medipac.ReadOnly;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Globalization;

namespace Medipac.Areas.CLI.Controllers
{
    [Area("CLI")]
    [Authorize(Roles = "Administrador")]
    public class CliMedicoController : Controller
    {
        private readonly ICliMedicoRepository climedico;
        private readonly IComUsuarioRepository comUsuario;
        private readonly IResEspecialidadesRepository resEspecialidades;
        private readonly IResMedicoEspecialidadRepository resMedicoEspecialidad;
        private readonly UserManager<ComUsuario> userManager;

        public CliMedicoController(ICliMedicoRepository climedico,
                                   IComUsuarioRepository comUsuario,
                                   IResEspecialidadesRepository resEspecialidades,
                                   IResMedicoEspecialidadRepository resMedicoEspecialidad,
                                   UserManager<ComUsuario> userManager)
        {
            this.climedico = climedico;
            this.comUsuario = comUsuario;
            this.resEspecialidades = resEspecialidades;
            this.resMedicoEspecialidad = resMedicoEspecialidad;
            this.userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            ViewData["ActivePage"] = "GestionarMedicos";
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
            ViewData["ActivePage"] = "GestionarMedicos";
            ViewBag.Estado = DropDownList.Estado;

            var especialidades = await resEspecialidades.GetAll();
            ViewBag.Especialidades = especialidades.Select(e => new { e.IdEspecialidad, e.Nombre }).ToList();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoCliMedico Dto)
        {
            // Generar el dígito verificador del RUT y almacenarlo
            Dto.Dv = GetFunctions.CalcularDvRut(Dto.Rut);

            // Generar el nombre de usuario y la contraseña
            string username = $"{RemoverTildes(Dto.Nombres.Split(' ')[0])}.{RemoverTildes(Dto.ApPaterno)}".ToLower();
            string password = Dto.Rut.ToString(); // RUT sin dígito verificador como contraseña

            // Crear el usuario de ASP.NET Identity
            var user = new ComUsuario
            {
                UserName = username,
                Email = Dto.Correo,
                EmailConfirmed = true,
                FechaCreacion = DateTime.Now,
                IdEstado = 1
            };

            // Guardar configuración original de las opciones de contraseña
            var originalPasswordOptions = userManager.Options.Password;

            // Desactivación de las restricciones de contraseña tempralmente
            userManager.Options.Password.RequireDigit = false;
            userManager.Options.Password.RequiredLength = 1;
            userManager.Options.Password.RequireNonAlphanumeric = false;
            userManager.Options.Password.RequireUppercase = false;
            userManager.Options.Password.RequireLowercase = false;

            var userResult = await userManager.CreateAsync(user, password);

            // Restaurar las opciones originales de contraseña
            userManager.Options.Password = originalPasswordOptions;

            if (!userResult.Succeeded)
            {
                // Si falla la creación del usuario, muestra los errores y no continúa
                foreach (var error in userResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(Dto);
            }

            try
            {
                // Asignar el Id del usuario al médico
                Dto.IdUsuario = user.Id;

                // Crear el médico en la base de datos con el Id de usuario asignado
                var medico = await climedico.Add(Dto.ToOriginal());
                var result = await climedico.Save();

                if (result > 0)
                {
                    // Asignar el rol "Médico" al usuario
                    await userManager.AddToRoleAsync(user, "Medico");

                    // Guardar las especialidades seleccionadas
                    if (Dto.EspecialidadesSeleccionadas != null && Dto.EspecialidadesSeleccionadas.Any())
                    {
                        foreach (var especialidadId in Dto.EspecialidadesSeleccionadas)
                        {
                            var medicoEspecialidad = new ResMedicoEspecialidad
                            {
                                IdMedico = medico.IdMedico,
                                IdEspecialidad = especialidadId
                            };
                            await resMedicoEspecialidad.Add(medicoEspecialidad);
                        }
                        await resMedicoEspecialidad.Save();
                    }

                    // Redirigir a la lista de médicos si todo salió bien
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Si ocurre un problema al guardar el médico, elimina el usuario creado
                    await userManager.DeleteAsync(user);
                    ModelState.AddModelError(string.Empty, "No se pudo crear el médico. El usuario ha sido eliminado.");
                    return View(Dto);
                }
            }
            catch (Exception)
            {
                // Si hay alguna excepción, elimina el usuario para mantener la consistencia
                await userManager.DeleteAsync(user);
                ModelState.AddModelError(string.Empty, "Ocurrió un error inesperado al crear el médico. El usuario ha sido eliminado.");
                return View(Dto);
            }
        }



        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            ViewData["ActivePage"] = "GestionarMedicos";
            ViewBag.Estado = DropDownList.Estado;

            var Query = await climedico.GetById(id);

            if (Query == null)
            {
                return NotFound();
            }

            var especialidades = await resEspecialidades.GetAll();
            ViewBag.Especialidades = especialidades
                .Select(e => new { e.IdEspecialidad, e.Nombre })
                .ToList();

            var espSelIds = Query.ResMedicoEspecialidad
                                 .Select(m => m.IdEspecialidad)
                                 .ToList();

            var espSelNombres = Query.ResMedicoEspecialidad
                                     .Select(m => m.IdEspecialidadNavigation.Nombre)
                                     .ToList();

            var queryDto = Query.ToDto();
            queryDto.EspecialidadesSeleccionadas = espSelIds;
            queryDto.NombresEspecialidades = espSelNombres;

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

            dto.Dv = GetFunctions.CalcularDvRut(dto.Rut);

            var medico = await climedico.GetById(id);

            if (medico == null)
            {
                return NotFound();
            }

            medico.ResMedicoEspecialidad.Clear();

            foreach (var especialidadId in dto.EspecialidadesSeleccionadas)
            {
                medico.ResMedicoEspecialidad.Add(new ResMedicoEspecialidad
                {
                    IdMedico = medico.IdMedico,
                    IdEspecialidad = especialidadId
                });
            }

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

        public static string RemoverTildes(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
