using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Models;

[Table("ADM_Admin")]
public partial class AdmAdmin
{
    [Key]
    [Column("Id_Admin")]
    public int IdAdmin { get; set; }

    [Column("Id_Usuario")]
    public int IdUsuario { get; set; }

    [InverseProperty("IdAdminNavigation")]
    public virtual ICollection<AdmNoticias> AdmNoticias { get; set; } = new List<AdmNoticias>();

    [ForeignKey("IdUsuario")]
    [InverseProperty("AdmAdmin")]
    public virtual ComUsuario IdUsuarioNavigation { get; set; } = null!;
}
