using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Data.DTO
{
    public class DtoCliPacientes
    {
        [Key]
        [Column("Id_Paciente")]
        public int IdPaciente { get; set; }
        [Column("Id_Usuario")]
        public int IdUsuario { get; set; }
        [StringLength(100)]
        public string Nombres { get; set; } = null!;
        [Column("Ap_Paterno")]
        [StringLength(50)]
        public string ApPaterno { get; set; } = null!;
        [Column("Ap_Materno")]
        [StringLength(50)]
        public string? ApMaterno { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string Rut { get; set; } = null!;
        [StringLength(20)]
        public string Fono { get; set; } = null!;
        [StringLength(100)]
        public string Correo { get; set; } = null!;
        public bool Estado { get; set; }
    }
}