using Medipac.Data.DTO;
using Medipac.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Controllers
{
    public class ResReservaController : Controller
    {
        private readonly IResReservaRepository resreserva;

        public ResReservaController(IResReservaRepository resreserva)
        {
            this.resreserva = resreserva;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await resreserva.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await resreserva.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoResReserva Dto)
        {
            var Query = await resreserva.Add(Dto.ToOriginal());
            var Result = await resreserva.Save();

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

            var Query = await resreserva.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoResReserva dto)
        {
            if (id != dto.Id) { return NotFound(); }

            resreserva.Update(dto.ToOriginal());
            _ = await resreserva.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await resreserva.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resreserva = await resreserva.DeleteById(id);
            var Result = await resreserva.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}