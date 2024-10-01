using Medipac.Data.DTO;
using Medipac.Data.Interfaces;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Controllers
{
    public class ResCentroMedicoController : Controller
    {
        private readonly IResCentroMedicoRepository rescentromedico;

        public ResCentroMedicoController(IResCentroMedicoRepository rescentromedico)
        {
            this.rescentromedico = rescentromedico;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await rescentromedico.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await rescentromedico.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoResCentroMedico Dto)
        {
            var Query = await rescentromedico.Add(Dto.ToOriginal());
            var Result = await rescentromedico.Save();

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

            var Query = await rescentromedico.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoResCentroMedico dto)
        {
            if (id != dto.IdCentroMedico) { return NotFound(); }

            rescentromedico.Update(dto.ToOriginal());
            _ = await rescentromedico.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await rescentromedico.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Query = await rescentromedico.DeleteById(id);
            var Result = await rescentromedico.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}