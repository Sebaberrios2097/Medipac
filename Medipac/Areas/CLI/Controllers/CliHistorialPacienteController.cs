using Medipac.Areas.CLI.Data.DTO;
using Medipac.Areas.CLI.Data.Interfaces;
using Medipac.Models;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Areas.CLI.Controllers
{
    [Area("CLI")]
    public class CliHistorialPacienteController : Controller
    {
        private readonly ICliHistorialPacienteRepository clihistorialpaciente;

        public CliHistorialPacienteController(ICliHistorialPacienteRepository clihistorialpaciente)
        {
            this.clihistorialpaciente = clihistorialpaciente;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await clihistorialpaciente.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await clihistorialpaciente.GetById(id);
            return View(Query.ToDto());
        }

        [HttpPost]
        public async Task<JsonResult> CrearHistorialMedico(int idMedico, int idPaciente)
        {
            try
            {
                // Verificar si ya existe un historial médico para este médico y paciente
                var historialExistente = await clihistorialpaciente.GetHistorialByIdMedicoYPaciente(idMedico, idPaciente);

                if (historialExistente != null)
                {
                    return Json(new { success = false, message = "El historial médico ya existe para este médico y paciente." });
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



        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var Query = await clihistorialpaciente.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoCliHistorialPaciente dto)
        {
            if (id != dto.IdHistorialPaciente) { return NotFound(); }

            clihistorialpaciente.Update(dto.ToOriginal());
            _ = await clihistorialpaciente.Save();

            return RedirectToAction(nameof(Index));
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