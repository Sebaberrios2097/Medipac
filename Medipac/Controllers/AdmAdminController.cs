using Medipac.Data.DTO;
using Medipac.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Controllers
{
    public class AdmAdminController : Controller
    {
        private readonly IAdmAdminRepository admadmin;

        public AdmAdminController(IAdmAdminRepository admadmin)
        {
            this.admadmin = admadmin;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await admadmin.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await admadmin.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoAdmAdmin Dto)
        {
            var Query = await admadmin.Add(Dto.ToOriginal());
            var Result = await admadmin.Save();

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

            var Query = await admadmin.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoAdmAdmin dto)
        {
            if (id != dto.Id) { return NotFound(); }

            admadmin.Update(dto.ToOriginal());
            _ = await admadmin.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await admadmin.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admadmin = await admadmin.DeleteById(id);
            var Result = await admadmin.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}