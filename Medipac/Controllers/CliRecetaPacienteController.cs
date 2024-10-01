using Medipac.Data.DTO;
using Medipac.Data.Interfaces;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Controllers
{
    public class CliRecetaPacienteController : Controller
    {
        private readonly ICliRecetaPacienteRepository clirecetapaciente;

        public CliRecetaPacienteController(ICliRecetaPacienteRepository clirecetapaciente)
        {
            this.clirecetapaciente = clirecetapaciente;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await clirecetapaciente.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await clirecetapaciente.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoCliRecetaPaciente Dto)
        {
            var Query = await clirecetapaciente.Add(Dto.ToOriginal());
            var Result = await clirecetapaciente.Save();

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

            var Query = await clirecetapaciente.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoCliRecetaPaciente dto)
        {
            if (id != dto.IdRecetaPaciente) { return NotFound(); }

            clirecetapaciente.Update(dto.ToOriginal());
            _ = await clirecetapaciente.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await clirecetapaciente.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Query = await clirecetapaciente.DeleteById(id);
            var Result = await clirecetapaciente.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}