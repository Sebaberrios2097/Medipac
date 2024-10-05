using Medipac.Areas.RES.Data.DTO;
using Medipac.Areas.RES.Data.Interfaces;
using Medipac.ReadOnly;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Medipac.Areas.RES.Controllers
{
    [Area("RES")]
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
            ViewBag.Estado = DropDownList.Estado;
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
            if (id != dto.IdEspecialidad) { return NotFound(); }

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
            var Query = await resespecialidades.DeleteById(id);
            var Result = await resespecialidades.Save();

            return RedirectToAction(nameof(Index));
        }



    }
}