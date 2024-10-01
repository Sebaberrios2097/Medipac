using Medipac.Data.DTO;
using Medipac.Data.Interfaces;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Controllers
{
    public class CliPacientesController : Controller
    {
        private readonly ICliPacientesRepository clipacientes;

        public CliPacientesController(ICliPacientesRepository clipacientes)
        {
            this.clipacientes = clipacientes;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await clipacientes.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await clipacientes.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoCliPacientes Dto)
        {
            var Query = await clipacientes.Add(Dto.ToOriginal());
            var Result = await clipacientes.Save();

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

            var Query = await clipacientes.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoCliPacientes dto)
        {
            if (id != dto.IdPaciente) { return NotFound(); }

            clipacientes.Update(dto.ToOriginal());
            _ = await clipacientes.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await clipacientes.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Query = await clipacientes.DeleteById(id);
            var Result = await clipacientes.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}