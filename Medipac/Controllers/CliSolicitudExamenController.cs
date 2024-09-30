using Medipac.Data.DTO;
using Medipac.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Controllers
{
    public class CliSolicitudExamenController : Controller
    {
        private readonly ICliSolicitudExamenRepository clisolicitudexamen;

        public CliSolicitudExamenController(ICliSolicitudExamenRepository clisolicitudexamen)
        {
            this.clisolicitudexamen = clisolicitudexamen;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await clisolicitudexamen.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await clisolicitudexamen.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoCliSolicitudExamen Dto)
        {
            var Query = await clisolicitudexamen.Add(Dto.ToOriginal());
            var Result = await clisolicitudexamen.Save();

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

            var Query = await clisolicitudexamen.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoCliSolicitudExamen dto)
        {
            if (id != dto.Id) { return NotFound(); }

            clisolicitudexamen.Update(dto.ToOriginal());
            _ = await clisolicitudexamen.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await clisolicitudexamen.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clisolicitudexamen = await clisolicitudexamen.DeleteById(id);
            var Result = await clisolicitudexamen.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}