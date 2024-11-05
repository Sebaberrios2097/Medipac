using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Models;

[Table("Res_HorarioMedico")]
public partial class ResHorarioMedico
{
    [Key]
    [Column("Id_Horario")]
    public int IdHorario { get; set; }

    [Column("Id_Medico")]
    public int IdMedico { get; set; }

    public int DiaSemana { get; set; }

    [Column("HoraInicio")]
    public int HoraInicio { get; set; }

    [Column("HoraFIn")]
    public int HoraFin { get; set; }

    [ForeignKey("IdMedico")]
    [InverseProperty("ResHorarioMedico")]
    public virtual CliMedico IdMedicoNavigation { get; set; } = null!;
}
