using Medipac.Data.ADM.DTO;
using Medipac.Data.ADM.Interfaces;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Areas.ADM.Controllers
{
    [Area("ADM")]
    public class AdmCarruselNoticiasController : Controller
    {
        private readonly IAdmCarruselNoticiasRepository admcarruselnoticias;

        public AdmCarruselNoticiasController(IAdmCarruselNoticiasRepository admcarruselnoticias)
        {
            this.admcarruselnoticias = admcarruselnoticias;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await admcarruselnoticias.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await admcarruselnoticias.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoCarruselNoticias Dto)
        {
            var Query = await admcarruselnoticias.Add(Dto.ToOriginal());
            var Result = await admcarruselnoticias.Save();

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

            var Query = await admcarruselnoticias.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoCarruselNoticias dto)
        {
            if (id != dto.IdCarruselNoticias) { return NotFound(); }

            admcarruselnoticias.Update(dto.ToOriginal());
            _ = await admcarruselnoticias.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await admcarruselnoticias.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Query = await admcarruselnoticias.DeleteById(id);
            var Result = await admcarruselnoticias.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}