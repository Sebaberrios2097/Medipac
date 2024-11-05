using Medipac.Areas.RES.Data.Interfaces;
using Medipac.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Medipac.Areas.CLI.Data.Interfaces;
using Medipac.ReadOnly.DtoTransformation;
using Medipac.Areas.RES.Data.DTO;

namespace Medipac.Areas.RES.Controllers
{
    [Area("RES")]
    public class ResHorarioMedicoController : Controller
    {
        private readonly IResHorarioMedicoRepository resHorarioMedicoRepository;
        private readonly ICliMedicoRepository cliMedicoRepository;
        private readonly UserManager<ComUsuario> userManager;

        public ResHorarioMedicoController(
            IResHorarioMedicoRepository resHorarioMedicoRepository,
            ICliMedicoRepository cliMedicoRepository,
            UserManager<ComUsuario> userManager)
        {
            this.resHorarioMedicoRepository = resHorarioMedicoRepository;
            this.cliMedicoRepository = cliMedicoRepository;
            this.userManager = userManager;
        }

        // Método para mostrar la lista de horarios de un médico
        public async Task<ActionResult> Index()
        {
            var usuarioId = userManager.GetUserId(User);
            var medico = await cliMedicoRepository.GetByUserId(usuarioId);

            if (medico == null)
            {
                return NotFound("Médico no encontrado.");
            }

            var horarios = await resHorarioMedicoRepository.GetByMedicoId(medico.IdMedico);

            return View(horarios.Select(c => c.ToDto()).ToList());
        }

        // Método para mostrar el formulario de creación de horario
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // Método para manejar el POST de creación de horario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoResHorarioMedico dto)
        {
            if (ModelState.IsValid)
            {
                var usuarioId = userManager.GetUserId(User);
                var medico = await cliMedicoRepository.GetByUserId(usuarioId);

                if (medico == null)
                {
                    return Json(new { success = false, title = "Error", message = "Médico no encontrado.", type = "error" });
                }

                dto.HoraInicio = Convert.ToInt32(TimeSpan.Parse(dto.HoraInicioDisplay).ToString("hhmm"));
                dto.HoraFin = Convert.ToInt32(TimeSpan.Parse(dto.HoraFinDisplay).ToString("hhmm"));
                dto.IdMedico = medico.IdMedico;

                await resHorarioMedicoRepository.Add(dto.ToOriginal());
                var result = await resHorarioMedicoRepository.Save();

                if (result > 0)
                {
                    return Json(new { success = true, title = "Horario Creado", message = "El horario fue creado exitosamente.", type = "success" });
                }
            }

            var errorMessage = string.Join("<br/>", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return Json(new { success = false, title = "Error en la creación", message = errorMessage, type = "error" });
        }



        // Método para mostrar el formulario de edición de horario
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var horario = await resHorarioMedicoRepository.GetById(id);
            if (horario == null) return NotFound();

            return View(horario);
        }

        // Método para manejar el POST de edición de horario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, DtoResHorarioMedico dto)
        {
            if (id != dto.IdHorario)
            {
                return Json(new { success = false, message = "ID de horario no coincide." });
            }

            if (ModelState.IsValid)
            {
                var usuarioId = userManager.GetUserId(User);
                var medico = await cliMedicoRepository.GetByUserId(usuarioId);

                if (medico == null)
                {
                    return Json(new { success = false, message = "Médico no encontrado." });
                }

                dto.IdMedico = medico.IdMedico;
                resHorarioMedicoRepository.Update(dto.ToOriginal());
                var result = await resHorarioMedicoRepository.Save();

                if (result > 0)
                {
                    return Json(new { success = true });
                }
            }

            var errorMessage = string.Join("<br/>", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return Json(new { success = false, message = errorMessage });
        }

        // Método para mostrar la confirmación de eliminación de horario
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var horario = await resHorarioMedicoRepository.GetById(id);
            if (horario == null) return NotFound();

            return View(horario);
        }

        // Método para manejar el POST de eliminación de horario
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await resHorarioMedicoRepository.DeleteById(id);
            await resHorarioMedicoRepository.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
