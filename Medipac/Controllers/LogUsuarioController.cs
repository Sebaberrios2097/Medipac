using Medipac.Data.DTO;
using Medipac.Data.Interfaces;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Controllers
{
    public class LogUsuarioController : Controller
    {
        private readonly ILogUsuarioRepository logusuario;

        public LogUsuarioController(ILogUsuarioRepository logusuario)
        {
            this.logusuario = logusuario;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await logusuario.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await logusuario.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoLogUsuario Dto)
        {
            var Query = await logusuario.Add(Dto.ToOriginal());
            var Result = await logusuario.Save();

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

            var Query = await logusuario.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoLogUsuario dto)
        {
            if (id != dto.IdLogUsuario { return NotFound(); }

            logusuario.Update(dto.ToOriginal());
            _ = await logusuario.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await logusuario.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Query = await logusuario.DeleteById(id);
            var Result = await logusuario.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}