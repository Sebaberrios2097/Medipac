using Medipac.Data.DTO;
using Medipac.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Controllers
{
    public class ResEspecialidadesController : Controller
    {
        private readonly IResEspecialidadesRepository resespecialidades;

        public ResEspecialidadesController(IResEspecialidadesRepository resespecialidades)
        {
            this.resespecialidades = resespecialidades;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await resespecialidades.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await resespecialidades.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoResEspecialidades Dto)
        {
            var Query = await resespecialidades.Add(Dto.ToOriginal());
            var Result = await resespecialidades.Save();

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

            var Query = await resespecialidades.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoResEspecialidades dto)
        {
            if (id != dto.Id) { return NotFound(); }

            resespecialidades.Update(dto.ToOriginal());
            _ = await resespecialidades.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await resespecialidades.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resespecialidades = await resespecialidades.DeleteById(id);
            var Result = await resespecialidades.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}