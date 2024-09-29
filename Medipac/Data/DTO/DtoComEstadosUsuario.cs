using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Data.DTO
{
    public class DtoComEstadosUsuario
    {
        [Key]
        [Column("Id_Estado")]
        public int IdEstado { get; set; }
        [StringLength(50)]
        public string Descripcion { get; set; } = null!;
        public bool Estado { get; set; }
    }
}