using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Models;

[Table("CLI_Historial_Paciente")]
public partial class CliHistorialPaciente
{
    [Key]
    [Column("Id_Historial_Paciente")]
    public int IdHistorialPaciente { get; set; }

    [Column("Id_Paciente")]
    public int IdPaciente { get; set; }

    [Column("Id_Medico")]
    public int IdMedico { get; set; }

    [Column(TypeName = "text")]
    public string Historial { get; set; } = null!;

    [Column("Fecha_Creacion", TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [Column("Fecha_Historial", TypeName = "datetime")]
    public DateTime FechaHistorial { get; set; }

    /// <summary>
    /// Columna que representa el borrado lógico del registro.
    /// </summary>
    public bool Estado { get; set; }

    [InverseProperty("IdHistorialPacienteNavigation")]
    public virtual ICollection<CliRecetaPaciente> CliRecetaPaciente { get; set; } = new List<CliRecetaPaciente>();

    [InverseProperty("IdHistorialPacienteNavigation")]
    public virtual ICollection<CliSolicitudExamen> CliSolicitudExamen { get; set; } = new List<CliSolicitudExamen>();

    [ForeignKey("IdMedico")]
    [InverseProperty("CliHistorialPaciente")]
    public virtual CliMedico IdMedicoNavigation { get; set; } = null!;

    [ForeignKey("IdPaciente")]
    [InverseProperty("CliHistorialPaciente")]
    public virtual CliPacientes IdPacienteNavigation { get; set; } = null!;
}
