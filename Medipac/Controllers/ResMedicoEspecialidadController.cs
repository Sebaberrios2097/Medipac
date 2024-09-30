using Medipac.Data.DTO;
using Medipac.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Controllers
{
    public class ResMedicoEspecialidadController : Controller
    {
        private readonly IResMedicoEspecialidadRepository resmedicoespecialidad;

        public ResMedicoEspecialidadController(IResMedicoEspecialidadRepository resmedicoespecialidad)
        {
            this.resmedicoespecialidad = resmedicoespecialidad;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await resmedicoespecialidad.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await resmedicoespecialidad.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
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
            if (id != dto.Id) { return NotFound(); }

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
            var resmedicoespecialidad = await resmedicoespecialidad.DeleteById(id);
            var Result = await resmedicoespecialidad.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}