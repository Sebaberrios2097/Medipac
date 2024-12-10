using Medipac.Areas.CLI.Data.DTO;
using Medipac.Areas.CLI.Data.Interfaces;
using Medipac.Areas.CLI.ViewModels;
using Medipac.Models;
using Medipac.ReadOnly;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Areas.CLI.Controllers
{
    [Area("CLI")]
    public class CliHistorialPacienteController : Controller
    {
        private readonly ICliHistorialPacienteRepository clihistorialpaciente;
        private readonly ICliMedicoRepository climedico;
        private readonly ICliPacientesRepository clipacientes;
        private readonly UserManager<ComUsuario> userManager;

        public CliHistorialPacienteController(ICliHistorialPacienteRepository clihistorialpaciente,
                                              ICliMedicoRepository climedico,
                                              ICliPacientesRepository clipacientes,
                                              UserManager<ComUsuario> userManager)
        {
            this.clihistorialpaciente = clihistorialpaciente;
            this.climedico = climedico;
            this.clipacientes = clipacientes;
            this.userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await clihistorialpaciente.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var userId = userManager.GetUserId(User);

            ViewData["IdUsuario"] = userId;
            ViewData["ActivePage"] = "Historiales";
            var Query = await clihistorialpaciente.GetById(id);
            return View(Query.ToDto());
        }

        [HttpPost]
        [Authorize(Roles = "Medico")]
        public async Task<JsonResult> CrearHistorialMedico(int idMedico, int idPaciente, bool confirmarCreacion = false)
        {
            try
            {
                // Verificar si ya existe un historial médico para este médico y paciente en la fecha actual
                var historialExistente = await clihistorialpaciente.GetHistorialByIdMedicoYPaciente(idMedico, idPaciente);

                if (historialExistente != null && historialExistente.FechaHistorial.Date == DateTime.Now.Date)
                {
                    if (!confirmarCreacion)
                    {
                        return Json(new
                        {
                            success = false,
                            confirmar = true,
                            message = "Ya existe un historial médico para este médico y paciente en la fecha actual. ¿Está seguro de que desea crear otro?"
                        });
                    }
                }

                // Crear un nuevo historial médico
                var nuevoHistorial = new CliHistorialPaciente
                {
                    IdMedico = idMedico,
                    IdPaciente = idPaciente,
                    FechaHistorial = DateTime.Now,
                    FechaCreacion = DateTime.Now,
                    Historial = "",
                    Estado = true
                };

                await clihistorialpaciente.Add(nuevoHistorial);
                var result = await clihistorialpaciente.Save();

                if (result > 0)
                {
                    return Json(new { success = true, message = "Historial médico creado correctamente." });
                }

                return Json(new { success = false, message = "No se pudo crear el historial médico. Intente de nuevo." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Ocurrió un error al crear el historial médico.", error = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> VerHistorialesMedico(string id)
        {
            try
            {
                ViewData["ActivePage"] = "Historiales";
                var medico = await climedico.GetByUserId(id);
                var historiales = await clihistorialpaciente.GetHistorialesPorMedico(medico.IdMedico);

                var viewModel = historiales.Select(h => new HistorialMedicoViewModel
                {
                    IdHistorial = h.IdHistorialPaciente,
                    FechaHistorial = h.FechaHistorial.ToString("dd/MM/yyyy"),
                    PacienteNombre = h.IdPacienteNavigation != null
                        ? $"{h.IdPacienteNavigation.Nombres ?? "Sin nombre"} {h.IdPacienteNavigation.ApPaterno ?? ""} {h.IdPacienteNavigation.ApMaterno ?? ""}"
                        : "Sin nombre",
                    Historial = h.Historial,
                    Estado = h.Estado ? "Activo" : "Inactivo"
                }).ToList();

                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                ViewBag.ErrorMessage = "Ocurrió un error al obtener los historiales médicos.";
                return View("Error");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Paciente")]
        public async Task<IActionResult> VerHistorialesPaciente(string id)
        {
            try
            {
                ViewData["ActivePage"] = "Historiales";
                var paciente = await clipacientes.GetByUserId(id);
                var historiales = await clihistorialpaciente.GetHistorialesPorPaciente(paciente.IdPaciente);

                var viewModel = historiales.Select(h => new HistorialMedicoViewModel
                {
                    IdHistorial = h.IdHistorialPaciente,
                    FechaHistorial = h.FechaHistorial.ToString("dd/MM/yyyy"),
                    PacienteNombre = h.IdPacienteNavigation != null
                        ? $"{h.IdPacienteNavigation.Nombres ?? "Sin nombre"} {h.IdPacienteNavigation.ApPaterno ?? ""} {h.IdPacienteNavigation.ApMaterno ?? ""}"
                        : "Sin nombre",
                    MedicoNombre = h.IdMedicoNavigation != null
                        ? $"{h.IdMedicoNavigation.Nombres ?? "Sin nombre"} {h.IdMedicoNavigation.ApPaterno ?? ""} {h.IdMedicoNavigation.ApMaterno ?? ""}"
                        : "Sin nombre",
                    Historial = h.Historial,
                    Estado = h.Estado ? "Activo" : "Inactivo"
                }).ToList();

                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                ViewBag.ErrorMessage = "Ocurrió un error al obtener los historiales médicos.";
                return View("Error");
            }
        }



        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var userId = userManager.GetUserId(User);

            ViewData["ActivePage"] = "Historiales";
            ViewData["IdUsuario"] = userId;
            ViewBag.Estado = DropDownList.Estado;

            var Query = await clihistorialpaciente.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> Edit(int id, DtoCliHistorialPaciente dto)
        {
            if (id != dto.IdHistorialPaciente) { return NotFound(); }

            var medico = await climedico.GetById(dto.IdMedico);
            clihistorialpaciente.Update(dto.ToOriginal());
            _ = await clihistorialpaciente.Save();

            return RedirectToAction(nameof(VerHistorialesMedico), new { id = medico.IdUsuario });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await clihistorialpaciente.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Query = await clihistorialpaciente.DeleteById(id);
            var Result = await clihistorialpaciente.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}