using Medipac.Data.DTO;
using Medipac.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Controllers
{
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
        public async Task<ActionResult> Create(DtoAdmNoticias Dto)
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
        public async Task<IActionResult> Edit(int id, DtoAdmNoticias dto)
        {
            if (id != dto.Id) { return NotFound(); }

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
            var admnoticias = await admnoticias.DeleteById(id);
            var Result = await admnoticias.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}