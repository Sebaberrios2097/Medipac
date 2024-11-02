using Medipac.Areas.ADM.Data.DTO;
using Medipac.Data.ADM.Interfaces;
using Medipac.Models;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Medipac.Areas.ADM.Controllers
{
    [Area("ADM")]
    [Authorize(Roles = "Administrador")]
    public class AdmNoticiasController : Controller
    {
        private readonly IAdmNoticiasRepository admnoticias;
        private readonly UserManager<ComUsuario> _userManager;

        public AdmNoticiasController(IAdmNoticiasRepository admnoticias,
                                     UserManager<ComUsuario> userManager)
        {
            this.admnoticias = admnoticias;
            _userManager = userManager;
        }

        public async Task<ActionResult> Index()
        {
            ViewData["ActivePage"] = "GestionarNoticias";
            var Query = await admnoticias.GetAll();
            return PartialView(Query.Select(item => item.ToDto()).ToList());
        }

        public async Task<ActionResult> Details(int id)
        {
            var Query = await admnoticias.GetById(id);
            return View(Query.ToDto());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DtoNoticias Dto)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            Dto.IdUsuario = int.Parse(userId);
            if (Dto.UrlImagen.IsNullOrEmpty())
            {
                Dto.UrlImagen = "";
            }

            var Query = await admnoticias.Add(Dto.ToOriginal());
            var Result = await admnoticias.Save();

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

            var Query = await admnoticias.GetById(id);

            if (Query == null) { return NotFound(); }

            return PartialView(Query.ToDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DtoNoticias dto)
        {
            if (id != dto.IdNoticia) { return NotFound(); }

            admnoticias.Update(dto.ToOriginal());
            _ = await admnoticias.Save();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) { return NotFound(); }

            var Query = await admnoticias.GetById(id);

            if (Query == null) { return NotFound(); }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Query = await admnoticias.DeleteById(id);
            var Result = await admnoticias.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}