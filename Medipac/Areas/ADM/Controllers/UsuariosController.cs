using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Medipac.Models;
using Microsoft.AspNetCore.Authorization;
using Medipac.Areas.ADM.ViewModels;

namespace Medipac.Areas.ADM.Controllers
{
    [Area("ADM")]
    [Authorize(Roles = "Administrador")]
    public class UsuariosController : Controller
    {
        private readonly UserManager<ComUsuario> _userManager;

        public UsuariosController(UserManager<ComUsuario> userManager)
        {
            _userManager = userManager;
        }

        // Método para listar todos los usuarios y sus roles
        public async Task<IActionResult> Index()
        {
            ViewData["ActivePage"] = "GestionarUsuarios";

            var usuarios = _userManager.Users.ToList();
            var usuariosConRoles = new List<UsuarioConRolesViewModel>();

            foreach (var usuario in usuarios)
            {
                var roles = await _userManager.GetRolesAsync(usuario);
                usuariosConRoles.Add(new UsuarioConRolesViewModel
                {
                    Usuario = usuario,
                    Roles = roles
                });
            }

            return View(usuariosConRoles);
        }


        // Método para mostrar el formulario de creación de usuario
        public IActionResult Create()
        {
            return View();
        }

        // Método para crear un nuevo usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string userName, string email, string password)
        {
            if (ModelState.IsValid)
            {
                var usuario = new ComUsuario { UserName = userName, Email = email };
                var result = await _userManager.CreateAsync(usuario, password);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View();
        }

        // Método para mostrar el formulario de edición de usuario
        public async Task<IActionResult> Edit(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // Método para actualizar el usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, string userName, string email)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.UserName = userName;
            usuario.Email = email;

            var result = await _userManager.UpdateAsync(usuario);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(usuario);
        }

        // Método para eliminar un usuario
        public async Task<IActionResult> Delete(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // Confirmación para eliminar un usuario
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(usuario);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(usuario);
        }

        public async Task<IActionResult> Bloquear(int id)
        {
            var usuario = await _userManager.FindByIdAsync(id.ToString());

            return PartialView(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Bloquear(string id, int tiempoBloqueo)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            await _userManager.SetLockoutEndDateAsync(usuario, DateTimeOffset.UtcNow.AddMonths(tiempoBloqueo));

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Desbloquear(int id)
        {
            var usuario = await _userManager.FindByIdAsync(id.ToString());

            return PartialView(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Desbloquear(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            // Desbloquea al usuario inmediatamente
            await _userManager.SetLockoutEndDateAsync(usuario, null);

            return RedirectToAction(nameof(Index));
        }


    }
}
