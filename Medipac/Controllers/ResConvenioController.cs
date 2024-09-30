using Medipac.Data.DTO;
using Medipac.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Controllers
{
    public class ResConvenioController : Controller
    {
        private readonly IResConvenioRepository resconvenio;

        public ResConvenioController(IResConvenioRepository resconvenio)
        {
            this.resconvenio = resconvenio;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await resconvenio.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await resconvenio.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoResConvenio Dto)
        {
            var Query = await resconvenio.Add(Dto.ToOriginal());
            var Result = await resconvenio.Save();

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

            var Query = await resconvenio.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoResConvenio dto)
        {
            if (id != dto.Id) { return NotFound(); }

            resconvenio.Update(dto.ToOriginal());
            _ = await resconvenio.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await resconvenio.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resconvenio = await resconvenio.DeleteById(id);
            var Result = await resconvenio.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}