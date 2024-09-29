using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Data.DTO
{
    public class DtoResPrevisiones
    {
        [Key]
        [Column("Id_Prevision")]
        public int IdPrevision { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        public bool Estado { get; set; }
    }
}