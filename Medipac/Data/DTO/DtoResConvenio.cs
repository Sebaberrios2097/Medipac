using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Data.DTO
{
    public class DtoResConvenio
    {
        [Key]
        [Column("Id_Convenio")]
        public int IdConvenio { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [StringLength(25)]
        public string Tipo { get; set; } = null!;
        public bool Estado { get; set; }
    }
}