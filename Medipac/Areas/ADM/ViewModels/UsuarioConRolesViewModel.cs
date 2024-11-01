using Medipac.Models;

namespace Medipac.Areas.ADM.ViewModels
{
    public class UsuarioConRolesViewModel
    {
        public ComUsuario Usuario { get; set; } = null!;
        public IEnumerable<string> Roles { get; set; } = null!;
    }
}