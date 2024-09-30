using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Models;

[Table("ADM_Carrusel_Noticias")]
public partial class AdmCarruselNoticias
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

    [ForeignKey("IdNoticia")]
    [InverseProperty("AdmCarruselNoticias")]
    public virtual AdmNoticias IdNoticiaNavigation { get; set; } = null!;
}
