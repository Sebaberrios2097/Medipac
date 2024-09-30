using Medipac.Data.DTO;
using Medipac.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Controllers
{
    public class ResAgendaController : Controller
    {
        private readonly IResAgendaRepository resagenda;

        public ResAgendaController(IResAgendaRepository resagenda)
        {
            this.resagenda = resagenda;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await resagenda.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await resagenda.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoResAgenda Dto)
        {
            var Query = await resagenda.Add(Dto.ToOriginal());
            var Result = await resagenda.Save();

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

            var Query = await resagenda.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoResAgenda dto)
        {
            if (id != dto.Id) { return NotFound(); }

            resagenda.Update(dto.ToOriginal());
            _ = await resagenda.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await resagenda.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resagenda = await resagenda.DeleteById(id);
            var Result = await resagenda.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}