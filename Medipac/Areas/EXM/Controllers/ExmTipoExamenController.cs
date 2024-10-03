using Medipac.Areas.EXM.Data.DTO;
using Medipac.Areas.EXM.Data.Interfaces;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Areas.EXM.Controllers
{
    [Area("EXM")]
    public class ExmTipoExamenController : Controller
    {
        private readonly IExmTipoExamenRepository exmtipoexamen;

        public ExmTipoExamenController(IExmTipoExamenRepository exmtipoexamen)
        {
            this.exmtipoexamen = exmtipoexamen;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await exmtipoexamen.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await exmtipoexamen.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoExmTipoExamen Dto)
        {
            var Query = await exmtipoexamen.Add(Dto.ToOriginal());
            var Result = await exmtipoexamen.Save();

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

            var Query = await exmtipoexamen.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoExmTipoExamen dto)
        {
            if (id != dto.IdTipoExamen) { return NotFound(); }

            exmtipoexamen.Update(dto.ToOriginal());
            _ = await exmtipoexamen.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await exmtipoexamen.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Query = await exmtipoexamen.DeleteById(id);
            var Result = await exmtipoexamen.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}