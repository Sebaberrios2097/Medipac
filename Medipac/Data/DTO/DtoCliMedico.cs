using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Data.DTO
{
    public class DtoCliMedico
    {
        [Key]
        [Column("Id_Medico")]
        public int IdMedico { get; set; }
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
        [StringLength(20)]
        public string Fono { get; set; } = null!;
        [StringLength(100)]
        public string Correo { get; set; } = null!;
        public bool Estado { get; set; }
    }
}