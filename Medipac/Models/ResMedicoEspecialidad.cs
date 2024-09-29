using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Models;

[Table("RES_Medico_Especialidad")]
public partial class ResMedicoEspecialidad
{
    [Key]
    [Column("Id_Medico_Especialidad")]
    public int IdMedicoEspecialidad { get; set; }

    [Column("Id_Medico")]
    public int IdMedico { get; set; }

    [Column("Id_Especialidad")]
    public int IdEspecialidad { get; set; }

    [ForeignKey("IdEspecialidad")]
    [InverseProperty("ResMedicoEspecialidad")]
    public virtual ResEspecialidades IdEspecialidadNavigation { get; set; } = null!;

    [ForeignKey("IdMedico")]
    [InverseProperty("ResMedicoEspecialidad")]
    public virtual CliMedico IdMedicoNavigation { get; set; } = null!;
}
