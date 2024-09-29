using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Data.DTO
{
    public class DtoResEspecialidades
    {
        [Key]
        [Column("Id_Especialidad")]
        public int IdEspecialidad { get; set; }
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        public bool Estado { get; set; }
    }
}