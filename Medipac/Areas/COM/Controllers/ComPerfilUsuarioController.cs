using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Medipac.Areas.COM.Models;
using Medipac.Areas.CLI.Data.Interfaces;
using Medipac.Models;

namespace Medipac.Areas.COM.Controllers
{
    [Area("COM")]
    public class ComPerfilUsuarioController : Controller
    {
        private readonly UserManager<ComUsuario> _userManager;
        private readonly ICliMedicoRepository _cliMedico;
        private readonly ICliPacientesRepository _cliPacientes;

        public ComPerfilUsuarioController(UserManager<ComUsuario> userManager,
                                          ICliMedicoRepository cliMedico,
                                          ICliPacientesRepository cliPacientes)
        {
            _userManager = userManager;
            _cliMedico = cliMedico;
            _cliPacientes = cliPacientes;
        }

        public async Task<IActionResult> Index()
        {
            // Obtén el usuario actual
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account", new { area = "Identity" });

            // Crea el ViewModel
            var viewModel = new PerfilUsuarioViewModel
            {
                Usuario = user
            };

            // Obtén los roles del usuario
            var roles = await _userManager.GetRolesAsync(user);
            viewModel.Rol = roles.FirstOrDefault() ?? "Usuario";

            // Carga datos adicionales según el rol
            if (viewModel.Rol == "Paciente")
            {
                viewModel.CliPacientes = await _cliPacientes.GetByUserId(user.Id.ToString());
            }
            else if (viewModel.Rol == "Medico")
            {
                viewModel.CliMedico = await _cliMedico.GetByUserId(user.Id.ToString());
            }

            // Devuelve la vista con el ViewModel
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateGeneralInfo(string email, string phoneNumber)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            // Actualiza el correo
            if (user.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, email);
                if (!setEmailResult.Succeeded)
                {
                    TempData["Error"] = "Error al actualizar el correo.";
                    return RedirectToAction("Index");
                }
            }

            // Actualiza el teléfono
            if (user.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, phoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    TempData["Error"] = "Error al actualizar el número de teléfono.";
                    return RedirectToAction("Index");
                }
            }

            TempData["Success"] = "Información actualizada correctamente.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            if (newPassword != confirmPassword)
            {
                TempData["Error"] = "La nueva contraseña y la confirmación no coinciden.";
                return RedirectToAction("Index");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!changePasswordResult.Succeeded)
            {
                TempData["Error"] = "Error al cambiar la contraseña. Verifica la contraseña actual.";
                return RedirectToAction("Index");
            }

            TempData["Success"] = "Contraseña actualizada correctamente.";
            return RedirectToAction("Index");
        }
    }
}
