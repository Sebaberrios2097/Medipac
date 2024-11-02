using Medipac.Data.ADM.Interfaces;
using Medipac.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Medipac.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdmCarruselNoticiasRepository _admCarruselNoticiasRepository;

        public HomeController(ILogger<HomeController> logger,
                              IAdmCarruselNoticiasRepository admCarruselNoticiasRepository)
        {
            _logger = logger;
            _admCarruselNoticiasRepository = admCarruselNoticiasRepository;
        }

        public async Task<ActionResult> Index()
        {
            var sliders = await _admCarruselNoticiasRepository.GetAllActive();
            return View(sliders);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
