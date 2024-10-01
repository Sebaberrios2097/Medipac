using Medipac.Data.DTO;
using Medipac.Data.Interfaces;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Controllers
{
    public class CliExamenesSolicitadosController : Controller
    {
        private readonly ICliExamenesSolicitadosRepository cliexamenessolicitados;

        public CliExamenesSolicitadosController(ICliExamenesSolicitadosRepository cliexamenessolicitados)
        {
            this.cliexamenessolicitados = cliexamenessolicitados;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await cliexamenessolicitados.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await cliexamenessolicitados.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoCliExamenesSolicitados Dto)
        {
            var Query = await cliexamenessolicitados.Add(Dto.ToOriginal());
            var Result = await cliexamenessolicitados.Save();

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

            var Query = await cliexamenessolicitados.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoCliExamenesSolicitados dto)
        {
            if (id != dto.IdExamenesSolicitados) { return NotFound(); }

            cliexamenessolicitados.Update(dto.ToOriginal());
            _ = await cliexamenessolicitados.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await cliexamenessolicitados.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Query = await cliexamenessolicitados.DeleteById(id);
            var Result = await cliexamenessolicitados.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}