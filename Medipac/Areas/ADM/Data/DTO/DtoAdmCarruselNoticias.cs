using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Areas.ADM.Data.DTO
{
    public class DtoAdmCarruselNoticias
    {
        [Key]
        [Column("Id_Carrusel_Noticias")]
        public int IdCarruselNoticias { get; set; }
        [Column("Id_Noticia")]
        public int IdNoticia { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [StringLength(100)]
        public string Titulo { get; set; } = null!;
        [StringLength(100)]
        public string Subtitulo { get; set; } = null!;
        [Column("Url_Imagen")]
        [StringLength(500)]
        public string? UrlImagen { get; set; }

        public bool Activo { get; set; }
    }
}