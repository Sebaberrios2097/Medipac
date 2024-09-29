using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Models;

[Table("RES_Medico_Convenio")]
public partial class ResMedicoConvenio
{
    [Key]
    [Column("Id_Medico_Convenio")]
    public int IdMedicoConvenio { get; set; }

    [Column("Id_Convenio")]
    public int IdConvenio { get; set; }

    [Column("Id_Medico")]
    public int IdMedico { get; set; }

    [ForeignKey("IdConvenio")]
    [InverseProperty("ResMedicoConvenio")]
    public virtual ResConvenio IdConvenioNavigation { get; set; } = null!;

    [ForeignKey("IdMedico")]
    [InverseProperty("ResMedicoConvenio")]
    public virtual CliMedico IdMedicoNavigation { get; set; } = null!;
}
