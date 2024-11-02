using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Models
{
    public partial class ComUsuario : IdentityUser<int> // Indicamos que la llave primaria es de tipo int
    {
        [Column("Id_Estado")]
        public int IdEstado { get; set; }

        [Column("Fecha_Creacion", TypeName = "datetime")]
        public DateTime FechaCreacion { get; set; }

        [Column("Is_Admin")]
        public bool IsAdmin { get; set; }

        // Relaciones con otras entidades
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<AdmNoticias> AdmNoticias { get; set; } = new List<AdmNoticias>();

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<CliMedico> CliMedico { get; set; } = new List<CliMedico>();

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<CliPacientes> CliPacientes { get; set; } = new List<CliPacientes>();

        [ForeignKey("IdEstado")]
        [InverseProperty("ComUsuario")]
        public virtual ComEstadosUsuario IdEstadoNavigation { get; set; } = null!;

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<LogUsuario> LogUsuario { get; set; } = new List<LogUsuario>();
    }
}
