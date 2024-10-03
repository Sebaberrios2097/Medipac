using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Areas.EXM.Data.DTO
{
    public class DtoExmTipoExamen
    {
        [Key]
        [Column("Id_Tipo_Examen")]
        public int IdTipoExamen { get; set; }
        [Column("Id_Categoria_Examen")]
        public int IdCategoriaExamen { get; set; }
        [Column("Nombre_Tipo_Examen")]
        [StringLength(500)]
        public string NombreTipoExamen { get; set; } = null!;
        [Column("Codigo_Examen")]
        [StringLength(50)]
        public string CodigoExamen { get; set; } = null!;
        [Column("Texto_Adicional")]
        public bool TextoAdicional { get; set; }
    }
}