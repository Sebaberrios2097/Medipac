using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Data.DTO
{
    public class DtoExmCategoriaExamen
    {
        [Key]
        [Column("Id_Categoria_Examen")]
        public int IdCategoriaExamen { get; set; }
        [Column("Nombre_Categoria")]
        [StringLength(100)]
        public string NombreCategoria { get; set; } = null!;
        public bool Estado { get; set; }
    }
}