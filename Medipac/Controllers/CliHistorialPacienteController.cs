using Medipac.Data.DTO;
using Medipac.Data.Interfaces;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Controllers
{
    public class CliHistorialPacienteController : Controller
    {
        private readonly ICliHistorialPacienteRepository clihistorialpaciente;

        public CliHistorialPacienteController(ICliHistorialPacienteRepository clihistorialpaciente)
        {
            this.clihistorialpaciente = clihistorialpaciente;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await clihistorialpaciente.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await clihistorialpaciente.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoCliHistorialPaciente Dto)
        {
            var Query = await clihistorialpaciente.Add(Dto.ToOriginal());
            var Result = await clihistorialpaciente.Save();

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

            var Query = await clihistorialpaciente.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoCliHistorialPaciente dto)
        {
            if (id != dto.IdHistorialPaciente) { return NotFound(); }

            clihistorialpaciente.Update(dto.ToOriginal());
            _ = await clihistorialpaciente.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await clihistorialpaciente.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Query = await clihistorialpaciente.DeleteById(id);
            var Result = await clihistorialpaciente.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}