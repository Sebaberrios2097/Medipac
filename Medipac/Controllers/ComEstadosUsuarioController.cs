using Medipac.Data.DTO;
using Medipac.Data.Interfaces;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Controllers
{
    public class ComEstadosUsuarioController : Controller
    {
        private readonly IComEstadosUsuarioRepository comestadosusuario;

        public ComEstadosUsuarioController(IComEstadosUsuarioRepository comestadosusuario)
        {
            this.comestadosusuario = comestadosusuario;
        }

        public async Task<ActionResult> Index()
        {
            var Query = await comestadosusuario.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await comestadosusuario.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoComEstadosUsuario Dto)
        {
            var Query = await comestadosusuario.Add(Dto.ToOriginal());
            var Result = await comestadosusuario.Save();

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

            var Query = await comestadosusuario.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoComEstadosUsuario dto)
        {
            if (id != dto.IdEstado) { return NotFound(); }

            comestadosusuario.Update(dto.ToOriginal());
            _ = await comestadosusuario.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await comestadosusuario.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Query = await comestadosusuario.DeleteById(id);
            var Result = await comestadosusuario.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}