using Medipac.Models;
using Microsoft.AspNetCore.Identity;

namespace Medipac.Areas.COM.Models
{
    public class PerfilUsuarioViewModel
    {
        public ComUsuario? Usuario { get; set; }

        public CliMedico? CliMedico { get; set; }

        public CliPacientes? CliPacientes { get; set; }

        public string? Rol { get; set; }
    }
}
