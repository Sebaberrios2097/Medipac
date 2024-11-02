using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Models;

[Table("ADM_Noticias")]
public partial class AdmNoticias
{
    [Key]
    [Column("Id_Noticia")]
    public int IdNoticia { get; set; }

    [Column("Id_Usuario")]
    public int IdUsuario { get; set; }

    [StringLength(100)]
    public string Titulo { get; set; } = null!;

    [StringLength(100)]
    public string Subtitulo { get; set; } = null!;

    public string Contenido { get; set; } = null!;

    [Column("Fecha_Publicacion", TypeName = "datetime")]
    public DateTime FechaPublicacion { get; set; }

    [Column("Url_Imagen")]
    [StringLength(500)]
    public string? UrlImagen { get; set; }

    public bool Activo { get; set; }

    [InverseProperty("IdNoticiaNavigation")]
    public virtual ICollection<AdmCarruselNoticias> AdmCarruselNoticias { get; set; } = new List<AdmCarruselNoticias>();

    [ForeignKey("IdUsuario")]
    [InverseProperty("AdmNoticias")]
    public virtual ComUsuario IdUsuarioNavigation { get; set; } = null!;
}
