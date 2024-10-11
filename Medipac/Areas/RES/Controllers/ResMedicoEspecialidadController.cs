using Medipac.Areas.CLI.Data.Interfaces;
using Medipac.Areas.RES.Data.DTO;
using Medipac.Areas.RES.Data.Interfaces;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Medipac.Areas.RES.Controllers
{
    [Area("RES")]
    public class ResMedicoEspecialidadController : Controller
    {
        private readonly IResMedicoEspecialidadRepository resmedicoespecialidad;
        private readonly ICliMedicoRepository cliMedico;
        private readonly IResEspecialidadesRepository resEspecialidades;


        public ResMedicoEspecialidadController(IResMedicoEspecialidadRepository resmedicoespecialidad,
                                               ICliMedicoRepository cliMedico,
                                               IResEspecialidadesRepository resEspecialidades)
        {
            this.resmedicoespecialidad = resmedicoespecialidad;
            this.cliMedico = cliMedico;
            this.resEspecialidades = resEspecialidades;
        }

        public async Task<ActionResult> Index()
        {
            ViewData["ActivePage"] = "GestionarMedicoEspecialidades";
            var Query = await resmedicoespecialidad.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await resmedicoespecialidad.GetById(id);
            return View(Query.ToDto());
        }

        public async Task<ActionResult> Create()
        {
            ViewBag.Especialidad = new SelectList(await resEspecialidades.GetAll(), "IdEspecialidad", "Nombre");
            ViewBag.Medico = new SelectList(await cliMedico.GetAll(), "IdMedico", "Nombres");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoResMedicoEspecialidad Dto)
        {
            var Query = await resmedicoespecialidad.Add(Dto.ToOriginal());
            var Result = await resmedicoespecialidad.Save();

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

            var Query = await resmedicoespecialidad.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoResMedicoEspecialidad dto)
        {
            if (id != dto.IdMedicoEspecialidad) { return NotFound(); }

            resmedicoespecialidad.Update(dto.ToOriginal());
            _ = await resmedicoespecialidad.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await resmedicoespecialidad.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Query = await resmedicoespecialidad.DeleteById(id);
            var Result = await resmedicoespecialidad.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}