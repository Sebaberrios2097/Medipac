using Medipac.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Areas.ADM.Controllers
{
    [Area("ADM")]
    [Authorize(Roles = "Administrador")]  // Solo accesible para administradores
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<ComUsuario> _userManager;

        public RolesController(RoleManager<IdentityRole<int>> roleManager, UserManager<ComUsuario> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        // Listar todos los roles
        public async Task<IActionResult> Index()
        {
            ViewData["ActivePage"] = "GestionarRoles";
            var roles = _roleManager.Roles.ToList();
            var usuariosPorRol = new Dictionary<string, int>();

            foreach (var role in roles)
            {
                var userInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                usuariosPorRol[role.Name] = userInRole.Count();
            }
            ViewBag.UsuariosPorRol = usuariosPorRol;
            return View(roles);
        }

        // Crear un nuevo rol
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            if (!string.IsNullOrWhiteSpace(roleName))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole<int>(roleName));
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        // Editar un rol existente
        public async Task<IActionResult> Edit(int id)
        {
            var idstring = id.ToString();
            var role = await _roleManager.FindByIdAsync(idstring);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IdentityRole role)
        {
            var existingRole = await _roleManager.FindByIdAsync(role.Id);
            if (existingRole == null)
            {
                return NotFound();
            }

            existingRole.Name = role.Name;
            var result = await _roleManager.UpdateAsync(existingRole);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(role);
        }

        // Eliminar un rol
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "No se pudo eliminar el rol.");
            return View(role);
        }
    }
}