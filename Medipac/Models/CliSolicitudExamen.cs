using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Models;

[Table("CLI_Solicitud_Examen")]
public partial class CliSolicitudExamen
{
    [Key]
    [Column("Id_Solicitud_Examen")]
    public int IdSolicitudExamen { get; set; }

    [Column("Id_Historial_Paciente")]
    public int IdHistorialPaciente { get; set; }

    [Column("Fecha_Solicitud", TypeName = "datetime")]
    public DateTime FechaSolicitud { get; set; }

    [Column("Fecha_Creacion", TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [InverseProperty("IdSolicitudExamenNavigation")]
    public virtual ICollection<CliExamenesSolicitados> CliExamenesSolicitados { get; set; } = new List<CliExamenesSolicitados>();

    [ForeignKey("IdHistorialPaciente")]
    [InverseProperty("CliSolicitudExamen")]
    public virtual CliHistorialPaciente IdHistorialPacienteNavigation { get; set; } = null!;
}
