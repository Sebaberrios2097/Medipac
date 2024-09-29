using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Models;

[Table("LOG_Usuario")]
public partial class LogUsuario
{
    [Key]
    [Column("Id_Log_Usuario")]
    public int IdLogUsuario { get; set; }

    [Column("Id_Usuario")]
    public int IdUsuario { get; set; }

    [Column("Fecha_Creacion", TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    public bool Exitoso { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("LogUsuario")]
    public virtual ComUsuario IdUsuarioNavigation { get; set; } = null!;
}
