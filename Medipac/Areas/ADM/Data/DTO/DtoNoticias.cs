using System.ComponentModel.DataAnnotations;
namespace Medipac.Areas.ADM.Data.DTO
{
    public class DtoNoticias
    {
        [Key]
        public int IdNoticia { get; set; }
        public int IdUsuario { get; set; }
        [StringLength(100)]
        public string Titulo { get; set; } = null!;
        [StringLength(100)]
        public string Subtitulo { get; set; } = null!;
        public string Contenido { get; set; } = null!;
        public DateTime FechaPublicacion { get; set; } = DateTime.Now;
        [StringLength(500)]
        public string? UrlImagen { get; set; }
        public bool Activo { get; set; }
    }
}