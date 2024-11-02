using Medipac.Areas.COM.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Areas.ADM.Controllers
{
    [Area("ADM")]
    [Authorize(Roles = "Administrador")]
    public class ADMHomeController : Controller
    {
        private readonly IComUsuarioRepository comUsuarioRepository;

        public ADMHomeController(IComUsuarioRepository comUsuarioRepository)
        {
            this.comUsuarioRepository = comUsuarioRepository;
        }

        public ActionResult Home()
        {
            return View();
        }
    }
}
