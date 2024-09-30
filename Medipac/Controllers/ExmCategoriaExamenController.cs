using Medipac.Data.DTO;
using Medipac.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Controllers
{
    public class ExmCategoriaExamenController : Controller
    {
        private readonly IExmCategoriaExamenRepository exmcategoriaexamen;

        public ExmCategoriaExamenController(IExmCategoriaExamenRepository exmcategoriaexamen)
        {
            this.exmcategoriaexamen = exmcategoriaexamen;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await exmcategoriaexamen.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await exmcategoriaexamen.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoExmCategoriaExamen Dto)
        {
            var Query = await exmcategoriaexamen.Add(Dto.ToOriginal());
            var Result = await exmcategoriaexamen.Save();

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

            var Query = await exmcategoriaexamen.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoExmCategoriaExamen dto)
        {
            if (id != dto.Id) { return NotFound(); }

            exmcategoriaexamen.Update(dto.ToOriginal());
            _ = await exmcategoriaexamen.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await exmcategoriaexamen.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exmcategoriaexamen = await exmcategoriaexamen.DeleteById(id);
            var Result = await exmcategoriaexamen.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}