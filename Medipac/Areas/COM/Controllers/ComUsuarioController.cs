using Medipac.Areas.COM.Data.DTO;
using Medipac.Areas.COM.Data.Interfaces;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Mvc;


namespace Medipac.Areas.COM.Controllers
{
    [Area("COM")]
    public class ComUsuarioController : Controller
    {
        private readonly IComUsuarioRepository comusuario;

        public ComUsuarioController(IComUsuarioRepository comusuario)
        {
            this.comusuario = comusuario;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await comusuario.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await comusuario.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoComUsuario Dto)
        {
            var Query = await comusuario.Add(Dto.ToOriginal());
            var Result = await comusuario.Save();

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

            var Query = await comusuario.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoComUsuario dto)
        {
            if (id != dto.IdUsuario) { return NotFound(); }

            comusuario.Update(dto.ToOriginal());
            _ = await comusuario.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await comusuario.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Query = await comusuario.DeleteById(id);
            var Result = await comusuario.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}