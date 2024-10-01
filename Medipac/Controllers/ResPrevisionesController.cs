using Medipac.Data.DTO;
using Medipac.Data.Interfaces;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Controllers
{
    public class ResPrevisionesController : Controller
    {
        private readonly IResPrevisionesRepository resprevisiones;

        public ResPrevisionesController(IResPrevisionesRepository resprevisiones)
        {
            this.resprevisiones = resprevisiones;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await resprevisiones.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await resprevisiones.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoResPrevisiones Dto)
        {
            var Query = await resprevisiones.Add(Dto.ToOriginal());
            var Result = await resprevisiones.Save();

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

            var Query = await resprevisiones.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoResPrevisiones dto)
        {
            if (id != dto.IdPrevision) { return NotFound(); }

            resprevisiones.Update(dto.ToOriginal());
            _ = await resprevisiones.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await resprevisiones.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Query = await resprevisiones.DeleteById(id);
            var Result = await resprevisiones.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}