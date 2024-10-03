using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Areas.ADM.Data.DTO
{
    public class DtoAdmAdmin
    {
        [Key]
        [Column("Id_Admin")]
        public int IdAdmin { get; set; }
        [Column("Id_Usuario")]
        public int IdUsuario { get; set; }
    }
}