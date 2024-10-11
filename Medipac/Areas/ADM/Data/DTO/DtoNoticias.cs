using System.ComponentModel.DataAnnotations;
namespace Medipac.Data.ADM.DTO
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
        public DateTime FechaPublicacion { get; set; }
        [StringLength(500)]
        public string UrlImagen { get; set; } = null!;
        public bool Activo { get; set; }
    }
}