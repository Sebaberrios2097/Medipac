using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Data.DTO
{
    public class DtoLogUsuario
    {
        [Key]
        [Column("Id_Log_Usuario")]
        public int IdLogUsuario { get; set; }
        [Column("Id_Usuario")]
        public int IdUsuario { get; set; }
        [Column("Fecha_Creacion", TypeName = "datetime")]
        public DateTime FechaCreacion { get; set; }
        public bool Exitoso { get; set; }
    }
}