using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Models;

[Table("COM_Estados_Usuario")]
public partial class ComEstadosUsuario
{
    [Key]
    [Column("Id_Estado")]
    public int IdEstado { get; set; }

    [StringLength(50)]
    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// Columna que representa el borrado lógico del registro.
    /// </summary>
    public bool Estado { get; set; }

    [InverseProperty("IdEstadoNavigation")]
    public virtual ICollection<ComUsuario> ComUsuario { get; set; } = new List<ComUsuario>();
}
