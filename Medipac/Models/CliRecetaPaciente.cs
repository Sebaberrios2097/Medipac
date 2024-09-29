using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Models;

[Table("CLI_Receta_Paciente")]
public partial class CliRecetaPaciente
{
    [Key]
    [Column("Id_Receta_Paciente")]
    public int IdRecetaPaciente { get; set; }

    [Column("Id_Historial_Paciente")]
    public int IdHistorialPaciente { get; set; }

    [Column(TypeName = "text")]
    public string Receta { get; set; } = null!;

    /// <summary>
    /// Columna que representa el borrado lógico del registro.
    /// </summary>
    public bool Estado { get; set; }

    [ForeignKey("IdHistorialPaciente")]
    [InverseProperty("CliRecetaPaciente")]
    public virtual CliHistorialPaciente IdHistorialPacienteNavigation { get; set; } = null!;
}
