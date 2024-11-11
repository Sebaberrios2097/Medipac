using Medipac.Areas.CLI.Data.Interfaces;
using Medipac.Areas.RES.Data.DTO;
using Medipac.Areas.RES.Data.Interfaces;
using Medipac.Models;
using Medipac.ReadOnly;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

namespace Medipac.Areas.RES.Controllers
{
    [Area("RES")]
    [Authorize(Roles = "Medico")]
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
                    color = a.Disponible ? "#4CAF50" : "#F44336",
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

            ViewBag.Disponible = DropDownList.Disponible;

            return PartialView(model.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create(DtoResAgenda dto)
        {
            if (ModelState.IsValid)
            {
                var usuarioId = userManager.GetUserId(User);
                var medico = await cliMedico.GetByUserId(usuarioId);
                if (medico == null)
                {
                    return Json(new { success = false, message = "Médico no encontrado." });
                }

                // Verificar si existe un conflicto de horario
                bool existeConflicto = await resagenda.ExisteConflictoHorario(
                    medico.IdMedico,
                    dto.Fecha,
                    dto.HoraInicio,
                    dto.HoraFin
                );

                if (existeConflicto)
                {
                    return Json(new { success = false, message = "Ya existe una disponibilidad en este rango de horas." });
                }

                // Si no hay conflicto, procede a guardar
                dto.IdMedico = medico.IdMedico;
                var query = await resagenda.Add(dto.ToOriginal());
                var result = await resagenda.Save();

                if (result > 0)
                {
                    return Json(new { success = true });
                }
            }

            // Si el modelo no es válido, devolver errores de validación
            var errorMessage = string.Join("<br/>", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));

            return Json(new { success = false, message = errorMessage });
        }


        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            if (id == 0) return NotFound();

            var query = await resagenda.GetById(id);
            if (query == null) return NotFound();

            ViewBag.Disponible = DropDownList.Disponible;

            return PartialView("Edit", query.ToDto()); // Devuelve la vista Edit como parcial para el modal
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateRecurring(DtoResAgenda dto)
        {
            if (ModelState.IsValid)
            {
                var usuarioId = userManager.GetUserId(User);
                var medico = await cliMedico.GetByUserId(usuarioId);
                if (medico == null)
                {
                    return Json(new { success = false, message = "Médico no encontrado." });
                }

                dto.IdMedico = medico.IdMedico;

                DateOnly fechaFinRecurrente = dto.Fecha.AddMonths(1).AddDays(15);
                var mensajesConflictos = new List<string>(); // Lista para acumular los conflictos

                foreach (var dia in dto.DiasRecurrentes)
                {
                    var fechaActual = dto.Fecha;
                    while (fechaActual <= fechaFinRecurrente)
                    {
                        if (fechaActual.DayOfWeek == dia)
                        {
                            var nuevaDisponibilidad = new DtoResAgenda
                            {
                                Fecha = fechaActual,
                                HoraInicio = dto.HoraInicio,
                                HoraFin = dto.HoraFin,
                                Disponible = true,
                                IdMedico = medico.IdMedico
                            };

                            bool existeConflicto = await resagenda.ExisteConflictoHorario(
                                medico.IdMedico,
                                nuevaDisponibilidad.Fecha,
                                nuevaDisponibilidad.HoraInicio,
                                nuevaDisponibilidad.HoraFin
                            );

                            if (existeConflicto)
                            {
                                // Si hay conflicto, agrega un mensaje detallado para esta fecha y hora
                                var horaInicioStr = $"{nuevaDisponibilidad.HoraInicio / 100:D2}:{nuevaDisponibilidad.HoraInicio % 100:D2}";
                                var horaFinStr = $"{nuevaDisponibilidad.HoraFin / 100:D2}:{nuevaDisponibilidad.HoraFin % 100:D2}";
                                mensajesConflictos.Add($"Hay un conflicto de horario el día {fechaActual:dd/MM/yyyy} entre las {horaInicioStr} y {horaFinStr}.");
                            }
                            else
                            {
                                await resagenda.Add(nuevaDisponibilidad.ToOriginal());
                            }
                        }
                        fechaActual = fechaActual.AddDays(1);
                    }
                }

                var result = await resagenda.Save();

                if (mensajesConflictos.Any())
                {
                    // Si hubo conflictos, devolver los mensajes acumulados
                    return Json(new { success = false, message = string.Join("<br/>", mensajesConflictos) });
                }

                if (result > 0)
                {
                    return Json(new { success = true });
                }
            }

            var errorMessage = string.Join("<br/>", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));

            return Json(new { success = false, message = errorMessage });
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Edit(int idAgenda, DtoResAgenda dto)
        {
            if (idAgenda != dto.IdAgenda) return Json(new { success = false, message = "ID de disponibilidad no coincide." });

            if (ModelState.IsValid)
            {
                var usuarioId = userManager.GetUserId(User);
                var medico = await cliMedico.GetByUserId(usuarioId);
                if (medico == null)
                {
                    return Json(new { success = false, message = "Médico no encontrado." });
                }

                // Verificar si existe un conflicto de horario con otras disponibilidades
                bool existeConflicto = await resagenda.ExisteConflictoHorario(
                    medico.IdMedico,
                    dto.Fecha,
                    dto.HoraInicio,
                    dto.HoraFin,
                    dto.IdAgenda // Excluir la misma disponibilidad que estamos editando
                );

                if (existeConflicto)
                {
                    return Json(new { success = false, message = "Ya existe una disponibilidad en este rango de horas." });
                }

                // Si no hay conflicto, procede a actualizar
                dto.IdMedico = medico.IdMedico;
                resagenda.Update(dto.ToOriginal());
                var result = await resagenda.Save();

                if (result > 0)
                {
                    return Json(new { success = true });
                }
            }

            // Si el modelo no es válido, devolver errores de validación
            var errorMessage = string.Join("<br/>", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));

            return Json(new { success = false, message = errorMessage });
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
