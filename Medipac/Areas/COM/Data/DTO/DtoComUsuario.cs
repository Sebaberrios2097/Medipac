using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Areas.COM.Data.DTO
{
    public class DtoComUsuario
    {
        [Key]
        [Column("Id_Usuario")]
        public int IdUsuario { get; set; }
        [Column("Id_Estado")]
        public int IdEstado { get; set; }
        [StringLength(50)]
        public string Usuario { get; set; } = null!;
        [StringLength(50)]
        public string Password { get; set; } = null!;
        [Column("Fecha_Creacion", TypeName = "datetime")]
        public DateTime FechaCreacion { get; set; }
    }
}