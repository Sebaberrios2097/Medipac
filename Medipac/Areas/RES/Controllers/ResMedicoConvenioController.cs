using Medipac.Areas.RES.Data.DTO;
using Medipac.Areas.RES.Data.Interfaces;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Areas.RES.Controllers
{
    [Area("RES")]
    public class ResMedicoConvenioController : Controller
    {
        private readonly IResMedicoConvenioRepository resmedicoconvenio;

        public ResMedicoConvenioController(IResMedicoConvenioRepository resmedicoconvenio)
        {
            this.resmedicoconvenio = resmedicoconvenio;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await resmedicoconvenio.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await resmedicoconvenio.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoResMedicoConvenio Dto)
        {
            var Query = await resmedicoconvenio.Add(Dto.ToOriginal());
            var Result = await resmedicoconvenio.Save();

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

            var Query = await resmedicoconvenio.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoResMedicoConvenio dto)
        {
            if (id != dto.IdMedicoConvenio) { return NotFound(); }

            resmedicoconvenio.Update(dto.ToOriginal());
            _ = await resmedicoconvenio.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await resmedicoconvenio.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Query = await resmedicoconvenio.DeleteById(id);
            var Result = await resmedicoconvenio.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}