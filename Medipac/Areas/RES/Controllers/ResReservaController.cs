using Medipac.Areas.CLI.Data.Interfaces;
using Medipac.Areas.RES.Data.DTO;
using Medipac.Areas.RES.Data.Interfaces;
using Medipac.Data.Repositories;
using Medipac.Models;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Areas.RES.Controllers
{
    [Area("RES")]
    public class ResReservaController : Controller
    {
        private readonly IResReservaRepository resreserva;
        private readonly IResAgendaRepository resagenda;
        private readonly UserManager<ComUsuario> userManager;
        private readonly ICliPacientesRepository clipacientes;
        private readonly ICliMedicoRepository climedicos;
        private readonly IResEspecialidadesRepository resespecialidades;


        public ResReservaController(IResReservaRepository resreserva,
                                    IResAgendaRepository resagenda,
                                    UserManager<ComUsuario> userManager,
                                    ICliPacientesRepository clipacientes,
                                    IResEspecialidadesRepository resespecialidades,
                                    ICliMedicoRepository climedicos)
        {
            this.resreserva = resreserva;
            this.resagenda = resagenda;
            this.userManager = userManager;
            this.clipacientes = clipacientes;
            this.resespecialidades = resespecialidades;
            this.climedicos = climedicos;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await resreserva.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await resreserva.GetById(id);
            return View(Query.ToDto());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Create()
        {
            ViewBag.Especialidades = await resreserva.GetEspecialidades();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DtoResReserva dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Especialidades = await resreserva.GetEspecialidades();
                return View(dto);
            }

            var reserva = await resreserva.Add(dto.ToOriginal());
            var result = await resreserva.Save();

            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Especialidades = await resreserva.GetEspecialidades();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmReservation(int agendaId, int especialidadId)
        {
            var agenda = await resagenda.GetById(agendaId);
            if (agenda == null || !agenda.Disponible)
            {
                return Json(new { success = false, message = "La franja horaria ya no está disponible." });
            }

            var userId = userManager.GetUserId(User);

            var paciente = await clipacientes.GetByUserId(userId);

            var nuevaReserva = new ResReserva
            {
                IdPaciente = paciente.IdPaciente,
                IdMedico = agenda.IdMedico,
                Fecha = new DateTime(agenda.Fecha.Year, agenda.Fecha.Month, agenda.Fecha.Day, agenda.HoraInicio / 100, agenda.HoraInicio % 100, 0),
                Estado = true,
                FechaCreacion = DateTime.Now
            };

            await resreserva.Add(nuevaReserva);
            var result = await resreserva.Save();

            var especialidad = await resespecialidades.GetById(especialidadId);

            // Si la reserva se guarda correctamente, actualizamos la agenda para marcarla como no disponible
            if (result > 0)
            {
                agenda.Disponible = false; // Marcar la franja como no disponible
                agenda.Descripcion = $"Reserva para {especialidad.Nombre} solicitada por {paciente.ApPaterno} {paciente.ApMaterno} {paciente.Nombres}";
                resagenda.Update(agenda);
                await resagenda.Save();

                return Json(new { success = true, message = "Reserva confirmada con éxito." });
            }

            return Json(new { success = false, message = "Ocurrió un error al confirmar la reserva. Intente nuevamente." });
        }


        public async Task<JsonResult> GetMedicosPorEspecialidad(int especialidadId)
        {
            var medicos = await resreserva.GetMedicosByEspecialidad(especialidadId);
            return Json(medicos.Select(m => new { m.IdMedico, m.Nombres, m.ApPaterno, m.ApMaterno }));
        }

        [HttpGet]
        public async Task<JsonResult> GetDisponibilidadMedico(int medicoId)
        {
            var disponibilidades = await resagenda.GetDisponibilidadByMedicoAndDateRange(medicoId);

            var eventos = disponibilidades.Select(d => new
            {
                id = d.IdAgenda,
                title = d.Descripcion ?? "Disponible",
                medico = d.IdMedicoNavigation.Nombres,
                start = new DateTime(d.Fecha.Year, d.Fecha.Month, d.Fecha.Day, d.HoraInicio / 100, d.HoraInicio % 100, 0).ToString("s"),
                end = new DateTime(d.Fecha.Year, d.Fecha.Month, d.Fecha.Day, d.HoraFin / 100, d.HoraFin % 100, 0).ToString("s"),
                allDay = false
            });

            return Json(eventos);
        }


        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var Query = await resreserva.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoResReserva dto)
        {
            if (id != dto.IdReserva) { return NotFound(); }

            resreserva.Update(dto.ToOriginal());
            _ = await resreserva.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await resreserva.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Query = await resreserva.DeleteById(id);
            var Result = await resreserva.Save();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ReservasMedico()
        {
            ViewData["ActivePage"] = "Reservas";
            var userId = userManager.GetUserId(User);
            var medico = await climedicos.GetByUserId(userId);
            ViewData["IdMedico"] = medico.IdMedico;
            return View();
        }

        public async Task<IActionResult> ReservasPaciente()
        {
            ViewData["ActivePage"] = "Reservas";
            var userId = userManager.GetUserId(User);
            var paciente = await clipacientes.GetByUserId(userId);
            ViewData["IdPaciente"] = paciente.IdPaciente;
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetReservasPorMedico(int medicoId)
        {
            var reservas = await resreserva.GetByMedicoId(medicoId);

            if (reservas == null)
            {
                return Json(new List<object>()); // Retornar un array vacío si no hay reservas
            }

            var eventos = reservas.Select(r => new
            {
                id = r.IdReserva.ToString(),
                title = $"{r.IdPacienteNavigation.Nombres} {r.IdPacienteNavigation.ApPaterno} {r.IdPacienteNavigation.ApMaterno}",
                start = r.Fecha.ToString("yyyy-MM-ddTHH:mm:ss"),
                end = r.Fecha.AddMinutes(30).ToString("yyyy-MM-ddTHH:mm:ss"),
                description = $"Reserva con {r.IdPacienteNavigation.Nombres}",
                IdPaciente = r.IdPaciente
            });

            return Json(eventos);
        }

        [HttpGet]
        public async Task<JsonResult> GetReservasPorPaciente(int pacienteId)
        {
            var reservas = await resreserva.GetByPacienteId(pacienteId);

            if (reservas == null)
            {
                return Json(new List<object>()); // Retornar un array vacío si no hay reservas
            }

            var eventos = reservas.Select(r => new
            {
                id = r.IdReserva.ToString(),
                title = $"{r.IdMedicoNavigation.Nombres} {r.IdMedicoNavigation.ApPaterno} {r.IdMedicoNavigation.ApMaterno}",
                start = r.Fecha.ToString("yyyy-MM-ddTHH:mm:ss"),
                end = r.Fecha.AddMinutes(30).ToString("yyyy-MM-ddTHH:mm:ss"),
                description = $"Reserva con {r.IdMedicoNavigation.Nombres} {r.IdMedicoNavigation.ApPaterno} {r.IdMedicoNavigation.ApMaterno}"
            });

            return Json(eventos);
        }


    }
}