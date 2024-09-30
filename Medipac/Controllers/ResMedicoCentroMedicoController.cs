using Medipac.Data.DTO;
using Medipac.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Controllers
{
    public class ResMedicoCentroMedicoController : Controller
    {
        private readonly IResMedicoCentroMedicoRepository resmedicocentromedico;

        public ResMedicoCentroMedicoController(IResMedicoCentroMedicoRepository resmedicocentromedico)
        {
            this.resmedicocentromedico = resmedicocentromedico;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await resmedicocentromedico.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await resmedicocentromedico.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoResMedicoCentroMedico Dto)
        {
            var Query = await resmedicocentromedico.Add(Dto.ToOriginal());
            var Result = await resmedicocentromedico.Save();

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

            var Query = await resmedicocentromedico.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoResMedicoCentroMedico dto)
        {
            if (id != dto.Id) { return NotFound(); }

            resmedicocentromedico.Update(dto.ToOriginal());
            _ = await resmedicocentromedico.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await resmedicocentromedico.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resmedicocentromedico = await resmedicocentromedico.DeleteById(id);
            var Result = await resmedicocentromedico.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}