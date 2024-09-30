using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Models;

[Table("ADM_Noticias")]
public partial class AdmNoticias
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

    [InverseProperty("IdNoticiaNavigation")]
    public virtual ICollection<AdmCarruselNoticias> AdmCarruselNoticias { get; set; } = new List<AdmCarruselNoticias>();

    [ForeignKey("IdAdmin")]
    [InverseProperty("AdmNoticias")]
    public virtual AdmAdmin IdAdminNavigation { get; set; } = null!;
}
