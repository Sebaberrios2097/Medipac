using Medipac.Areas.ADM.ViewModels;
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
        private readonly IAdmNoticiasRepository _admNoticiasRepository;

        public HomeController(ILogger<HomeController> logger,
                              IAdmCarruselNoticiasRepository admCarruselNoticiasRepository,
                              IAdmNoticiasRepository admNoticiasRepository)
        {
            _logger = logger;
            _admCarruselNoticiasRepository = admCarruselNoticiasRepository;
            _admNoticiasRepository = admNoticiasRepository;
        }

        public async Task<ActionResult> Index()
        {
            var sliders = await _admCarruselNoticiasRepository.GetAllActive();
            var noticias = await _admNoticiasRepository.GetAllActive();
            var viewmodel = new NoticiasSlidersViewModels
            {
                AdmCarruselNoticias = sliders,
                AdmNoticias = noticias
            };
            return View(viewmodel);
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
