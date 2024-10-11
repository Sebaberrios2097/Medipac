using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Models;

[Table("RES_Reserva")]
public partial class ResReserva
{
    [Key]
    [Column("Id_Reserva")]
    public int IdReserva { get; set; }

    [Column("Id_Paciente")]
    public int IdPaciente { get; set; }

    [Column("Id_Medico")]
    public int IdMedico { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    /// <summary>
    /// Columna que representa el borrado lógico del registro.
    /// </summary>
    public bool Estado { get; set; }

    [Column("Fecha_Creacion", TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [ForeignKey("IdMedico")]
    [InverseProperty("ResReserva")]
    public virtual CliMedico IdMedicoNavigation { get; set; } = null!;

    [ForeignKey("IdPaciente")]
    [InverseProperty("ResReserva")]
    public virtual CliPacientes IdPacienteNavigation { get; set; } = null!;
}
