using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Models;

[Table("CLI_Pacientes")]
public partial class CliPacientes
{
    [Key]
    [Column("Id_Paciente")]
    public int IdPaciente { get; set; }

    [Column("Id_Usuario")]
    public int IdUsuario { get; set; }

    [StringLength(100)]
    public string Nombres { get; set; } = null!;

    [Column("Ap_Paterno")]
    [StringLength(50)]
    public string ApPaterno { get; set; } = null!;

    [Column("Ap_Materno")]
    [StringLength(50)]
    public string? ApMaterno { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Rut { get; set; } = null!;

    [StringLength(20)]
    public string Fono { get; set; } = null!;

    [StringLength(100)]
    public string Correo { get; set; } = null!;

    /// <summary>
    /// Columna que representa el borrado lógico del registro.
    /// </summary>
    public bool Estado { get; set; }

    [InverseProperty("IdPacienteNavigation")]
    public virtual ICollection<CliHistorialPaciente> CliHistorialPaciente { get; set; } = new List<CliHistorialPaciente>();

    [ForeignKey("IdUsuario")]
    [InverseProperty("CliPacientes")]
    public virtual ComUsuario IdUsuarioNavigation { get; set; } = null!;

    [InverseProperty("IdPacienteNavigation")]
    public virtual ICollection<ResReserva> ResReserva { get; set; } = new List<ResReserva>();
}
