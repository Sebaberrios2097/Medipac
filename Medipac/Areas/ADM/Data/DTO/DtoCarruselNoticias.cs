using System.ComponentModel.DataAnnotations;
namespace Medipac.Data.ADM.DTO
{
    public class DtoCarruselNoticias
    {
        [Key]
        public int IdCarruselNoticias { get; set; }
        public int IdNoticia { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [StringLength(100)]
        public string Titulo { get; set; } = null!;
        [StringLength(100)]
        public string Subtitulo { get; set; } = null!;
        [StringLength(500)]
        public string? UrlImagen { get; set; }
        public bool Activo { get; set; }
    }
}