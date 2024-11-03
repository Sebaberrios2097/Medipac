using Medipac.Areas.CLI.Data.Interfaces;
using Medipac.Areas.RES.Data.DTO;
using Medipac.Areas.RES.Data.Interfaces;
using Medipac.Models;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Medipac.Areas.RES.Controllers
{
    [Area("RES")]
    public class ResAgendaController : Controller
    {
        private readonly IResAgendaRepository resagenda;
        private readonly ICliMedicoRepository cliMedico;
        private readonly UserManager<ComUsuario> userManager;

        public ResAgendaController(IResAgendaRepository resagenda,
                                   UserManager<ComUsuario> userManager,
                                   ICliMedicoRepository cliMedico)
        {
            this.resagenda = resagenda;
            this.userManager = userManager;
            this.cliMedico = cliMedico;
        }

        // Método para renderizar la vista de la agenda
        public async Task<ActionResult> Index()
        {
            var usuarioId = userManager.GetUserId(User);
            var medico = await cliMedico.GetByUserId(usuarioId);
            if (medico == null)
            {
                return NotFound("Médico no encontrado.");
            }

            ViewData["ActivePage"] = "Agenda";
            ViewData["MedicoId"] = medico.IdMedico; // Pasar el ID del médico a la vista
            return View();
        }

        // Método para obtener eventos en JSON
        [HttpGet]
        public async Task<JsonResult> GetEvents()
        {
            var usuarioId = userManager.GetUserId(User);
            var medico = await cliMedico.GetByUserId(usuarioId);
            if (medico == null)
            {
                return Json(new { success = false, message = "Médico no encontrado." });
            }

            var agenda = await resagenda.GetAll();
            var agendaDelMedico = agenda
                .Where(a => a.IdMedico == medico.IdMedico)
                .Select(a => new
                {
                    id = a.IdAgenda,
                    title = a.Disponible ? "Disponible" : "No disponible",
                    start = $"{a.Fecha:yyyy-MM-dd}T{(a.HoraInicio / 100):D2}:{(a.HoraInicio % 100):D2}",
                    end = $"{a.Fecha:yyyy-MM-dd}T{(a.HoraFin / 100):D2}:{(a.HoraFin % 100):D2}",
                    color = a.Disponible ? "green" : "red",
                    description = a.Descripcion.IsNullOrEmpty() ? "" : a.Descripcion
                })
                .ToList();

            return Json(agendaDelMedico);
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await resagenda.GetById(id);
            return View(Query.ToDto());
        }

        [HttpGet]
        public ActionResult Create(DateTime? start, DateTime? end)
        {
            // Prellenar los campos de fecha y hora si se proporcionan desde el calendario
            var model = new ResAgenda
            {
                Fecha = start.HasValue ? DateOnly.FromDateTime(start.Value) : DateOnly.FromDateTime(DateTime.Now), // Conversión explícita
                HoraInicio = start.HasValue ? (start.Value.Hour * 100 + start.Value.Minute) : 900, // 9:00 am por defecto
                HoraFin = end.HasValue ? (end.Value.Hour * 100 + end.Value.Minute) : 1700, // 5:00 pm por defecto
                Disponible = true
            };

            return View(model.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoResAgenda dto)
        {
            if (ModelState.IsValid)
            {
                var usuarioId = userManager.GetUserId(User);
                var medico = await cliMedico.GetByUserId(usuarioId);
                if (medico == null)
                {
                    return NotFound("Médico no encontrado.");
                }

                // Asignar el IdMedico antes de guardar la disponibilidad
                dto.IdMedico = medico.IdMedico;
                var query = await resagenda.Add(dto.ToOriginal());
                var result = await resagenda.Save();

                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(dto); // Si hay errores, volver a la vista de creación
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0) return NotFound();

            var Query = await resagenda.GetById(id);
            if (Query == null) return NotFound();

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoResAgenda dto)
        {
            if (id != dto.IdAgenda) return NotFound();

            resagenda.Update(dto.ToOriginal());
            await resagenda.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return NotFound();

            var Query = await resagenda.GetById(id);
            if (Query == null) return NotFound();

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Query = await resagenda.DeleteById(id);
            await resagenda.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
