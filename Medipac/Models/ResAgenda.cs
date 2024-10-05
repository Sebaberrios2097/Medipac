using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Models;

[Table("RES_Agenda")]
public partial class ResAgenda
{
    [Key]
    [Column("Id_Agenda")]
    public int IdAgenda { get; set; }

    [Column("Id_Medico")]
    public int IdMedico { get; set; }

    public DateOnly Fecha { get; set; }

    [Column("Hora_Inicio")]
    public int HoraInicio { get; set; }

    [Column("Hora_FIn")]
    public int HoraFin { get; set; }

    public bool Disponible { get; set; }

    [ForeignKey("IdMedico")]
    [InverseProperty("ResAgenda")]
    public virtual CliMedico IdMedicoNavigation { get; set; } = null!;
}
