using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Models;

[Table("RES_Medico_Centro_Medico")]
public partial class ResMedicoCentroMedico
{
    [Key]
    [Column("Id_Medico_Centro_Medico")]
    public int IdMedicoCentroMedico { get; set; }

    [Column("Id_Medico")]
    public int IdMedico { get; set; }

    [Column("Id_Centro_Medico")]
    public int IdCentroMedico { get; set; }

    [ForeignKey("IdCentroMedico")]
    [InverseProperty("ResMedicoCentroMedico")]
    public virtual ResCentroMedico IdCentroMedicoNavigation { get; set; } = null!;

    [ForeignKey("IdMedico")]
    [InverseProperty("ResMedicoCentroMedico")]
    public virtual CliMedico IdMedicoNavigation { get; set; } = null!;
}
