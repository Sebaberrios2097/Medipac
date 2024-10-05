using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Models;

[Table("RES_Centro_Medico")]
public partial class ResCentroMedico
{
    [Key]
    [Column("Id_Centro_Medico")]
    public int IdCentroMedico { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [StringLength(250)]
    public string Direccion { get; set; } = null!;

    [StringLength(20)]
    public string? Telefono { get; set; }

    [StringLength(100)]
    public string? Correo { get; set; }

    /// <summary>
    /// Columna que representa el borrado lógico del registro.
    /// </summary>
    public bool Estado { get; set; }

    [InverseProperty("IdCentroMedicoNavigation")]
    public virtual ICollection<ResMedicoCentroMedico> ResMedicoCentroMedico { get; set; } = new List<ResMedicoCentroMedico>();

    [InverseProperty("IdCentroMedicoNavigation")]
    public virtual ICollection<ResReserva> ResReserva { get; set; } = new List<ResReserva>();
}
