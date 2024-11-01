using Medipac.Areas.CLI.Data.Interfaces;
using Medipac.Areas.CLI.ViewModels;
using Medipac.Models;
using Medipac.ReadOnly.DtoTransformation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Areas.CLI.Controllers
{
    [Area("CLI")]
    public class CliHomeController : Controller
    {
        private readonly ICliMedicoRepository cliMedicoRepository;
        private readonly UserManager<ComUsuario> _userManager;

        public CliHomeController(ICliMedicoRepository cliMedicoRepository, UserManager<ComUsuario> userManager)
        {
            this.cliMedicoRepository = cliMedicoRepository;
            _userManager = userManager;

        }

        public async Task<IActionResult> Home()
        {
            var userMedico = await _userManager.GetUserAsync(User);
            var medico = await cliMedicoRepository.GetByUserId(userMedico.Id.ToString());
            var medicoUser = new CliMedicoUserViewModel()
            {
                ComUsuario = userMedico,
                DtoCliMedico = medico.ToDto()
            };
            return View(medicoUser);
        }
    }
}
