using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Models;

[Table("COM_Usuario")]
public partial class ComUsuario
{
    [Key]
    [Column("Id_Usuario")]
    public int IdUsuario { get; set; }

    [Column("Id_Estado")]
    public int IdEstado { get; set; }

    [StringLength(50)]
    public string Usuario { get; set; } = null!;

    [StringLength(50)]
    public string Password { get; set; } = null!;

    [Column("Fecha_Creacion", TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<CliMedico> CliMedico { get; set; } = new List<CliMedico>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<CliPacientes> CliPacientes { get; set; } = new List<CliPacientes>();

    [ForeignKey("IdEstado")]
    [InverseProperty("ComUsuario")]
    public virtual ComEstadosUsuario IdEstadoNavigation { get; set; } = null!;

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<LogUsuario> LogUsuario { get; set; } = new List<LogUsuario>();
}
