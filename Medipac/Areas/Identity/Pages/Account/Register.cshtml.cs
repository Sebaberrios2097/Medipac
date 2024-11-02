#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Medipac.Context;
using Medipac.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Medipac.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ComUsuario> _signInManager;
        private readonly UserManager<ComUsuario> _userManager;
        private readonly IUserStore<ComUsuario> _userStore;
        private readonly IUserEmailStore<ComUsuario> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly DbMedipac _dbContext; // Inyecta el DbContext

        public RegisterModel(
            UserManager<ComUsuario> userManager,
            IUserStore<ComUsuario> userStore,
            SignInManager<ComUsuario> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            DbMedipac dbContext) // Recibe el DbContext como parámetro
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _dbContext = dbContext; // Asigna el DbContext
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Nombre de Usuario")]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Correo Electrónico")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} y máximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar Contraseña")]
            [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
            public string ConfirmPassword { get; set; }

            // Campos adicionales para paciente
            [Required]
            [Display(Name = "Nombres")]
            public string Nombres { get; set; }

            [Required]
            [Display(Name = "Apellido Paterno")]
            public string ApPaterno { get; set; }

            [Display(Name = "Apellido Materno")]
            public string ApMaterno { get; set; }

            [Required]
            [StringLength(8, ErrorMessage = "No ingresar el dígito verificador.")]
            [Display(Name = "Rut sin dígito verificador")]
            public string Rut { get; set; }

            [Display(Name = "Teléfono")]
            public string Fono { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                // Asignar propiedades adicionales del usuario
                user.IdEstado = 1;
                user.IsAdmin = false;
                user.FechaCreacion = DateTime.Now;

                await _userStore.SetUserNameAsync(user, Input.UserName, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    // Asignar el rol de "Paciente" al usuario recién creado
                    await _userManager.AddToRoleAsync(user, "Paciente");

                    // Crear el registro en CLI_Pacientes usando el Id del usuario recién creado
                    var paciente = new CliPacientes
                    {
                        IdUsuario = user.Id,         // Vincular el Id del usuario
                        Nombres = Input.Nombres,
                        ApPaterno = Input.ApPaterno,
                        ApMaterno = Input.ApMaterno,
                        Rut = Input.Rut,
                        Fono = Input.Fono,
                        Estado = true,
                        Correo = Input.Email
                    };

                    // Guardar el paciente en la base de datos
                    _dbContext.CliPacientes.Add(paciente);
                    await _dbContext.SaveChangesAsync();

                    // Confirmación del registro de usuario
                    _logger.LogInformation("Usuario creó una nueva cuenta con contraseña.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirma tu correo electrónico",
                        $"Por favor confirma tu cuenta haciendo <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clic aquí</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Si llegamos hasta aquí, algo falló, mostrar nuevamente el formulario
            return Page();
        }

        private ComUsuario CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ComUsuario>();
            }
            catch
            {
                throw new InvalidOperationException($"No se puede crear una instancia de '{nameof(ComUsuario)}'. " +
                    $"Asegúrate de que '{nameof(ComUsuario)}' no sea una clase abstracta y tenga un constructor sin parámetros, o alternativamente " +
                    $"sobreescribe la página de registro en /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ComUsuario> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("La interfaz de usuario predeterminada requiere un almacén de usuarios con soporte de correo electrónico.");
            }
            return (IUserEmailStore<ComUsuario>)_userStore;
        }
    }
}
