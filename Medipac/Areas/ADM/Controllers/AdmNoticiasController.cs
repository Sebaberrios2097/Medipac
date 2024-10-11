using Medipac.Data.ADM.DTO;
using Medipac.Data.ADM.Interfaces;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Areas.ADM.Controllers
{
    [Area("ADM")]
    public class AdmNoticiasController : Controller
    {
        private readonly IAdmNoticiasRepository admnoticias;

        public AdmNoticiasController(IAdmNoticiasRepository admnoticias)
        {
            this.admnoticias = admnoticias;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await admnoticias.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await admnoticias.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoNoticias Dto)
        {
            var Query = await admnoticias.Add(Dto.ToOriginal());
            var Result = await admnoticias.Save();

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

            var Query = await admnoticias.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoNoticias dto)
        {
            if (id != dto.IdNoticia) { return NotFound(); }

            admnoticias.Update(dto.ToOriginal());
            _ = await admnoticias.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await admnoticias.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Query = await admnoticias.DeleteById(id);
            var Result = await admnoticias.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}