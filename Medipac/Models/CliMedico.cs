using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Models;

[Table("CLI_Medico")]
public partial class CliMedico
{
    [Key]
    [Column("Id_Medico")]
    public int IdMedico { get; set; }

    [Column("Id_Usuario")]
    public int IdUsuario { get; set; }

    public int Rut { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string Dv { get; set; } = null!;

    [StringLength(100)]
    public string Nombres { get; set; } = null!;

    [Column("Ap_Paterno")]
    [StringLength(50)]
    public string ApPaterno { get; set; } = null!;

    [Column("Ap_Materno")]
    [StringLength(50)]
    public string? ApMaterno { get; set; }

    [StringLength(20)]
    public string Fono { get; set; } = null!;

    [StringLength(100)]
    public string Correo { get; set; } = null!;

    /// <summary>
    /// Columna que representa el borrado lógico del registro.
    /// </summary>
    public bool Estado { get; set; }

    [InverseProperty("IdMedicoNavigation")]
    public virtual ICollection<CliHistorialPaciente> CliHistorialPaciente { get; set; } = new List<CliHistorialPaciente>();

    [ForeignKey("IdUsuario")]
    [InverseProperty("CliMedico")]
    public virtual ComUsuario IdUsuarioNavigation { get; set; } = null!;

    [InverseProperty("IdMedicoNavigation")]
    public virtual ICollection<ResAgenda> ResAgenda { get; set; } = new List<ResAgenda>();

    [InverseProperty("IdMedicoNavigation")]
    public virtual ICollection<ResMedicoCentroMedico> ResMedicoCentroMedico { get; set; } = new List<ResMedicoCentroMedico>();

    [InverseProperty("IdMedicoNavigation")]
    public virtual ICollection<ResMedicoConvenio> ResMedicoConvenio { get; set; } = new List<ResMedicoConvenio>();

    [InverseProperty("IdMedicoNavigation")]
    public virtual ICollection<ResMedicoEspecialidad> ResMedicoEspecialidad { get; set; } = new List<ResMedicoEspecialidad>();

    [InverseProperty("IdMedicoNavigation")]
    public virtual ICollection<ResReserva> ResReserva { get; set; } = new List<ResReserva>();
}
