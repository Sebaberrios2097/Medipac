using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Areas.ADM.Data.DTO
{
    public class DtoAdmNoticias
    {
        [Key]
        [Column("Id_Noticia")]
        public int IdNoticia { get; set; }
        [Column("Id_Admin")]
        public int IdAdmin { get; set; }
        [StringLength(100)]
        public string Titulo { get; set; } = null!;
        [StringLength(100)]
        public string Subtitulo { get; set; } = null!;
        public string Contenido { get; set; } = null!;
        [Column("Fecha_Publicacion", TypeName = "datetime")]
        public DateTime FechaPublicacion { get; set; }
        [Column("Url_Imagen")]
        [StringLength(500)]
        public string UrlImagen { get; set; } = null!;
        public bool Activo { get; set; }
    }
}