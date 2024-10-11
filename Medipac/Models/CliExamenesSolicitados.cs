using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Models;

[Table("CLI_Examenes_Solicitados")]
public partial class CliExamenesSolicitados
{
    [Key]
    [Column("Id_Examenes_Solicitados")]
    public int IdExamenesSolicitados { get; set; }

    [Column("Id_Solicitud_Examen")]
    public int IdSolicitudExamen { get; set; }

    [Column("Id_Tipo_Examen")]
    public int IdTipoExamen { get; set; }

    [StringLength(500)]
    public string? Descripcion { get; set; }

    [Column("Fecha_Creacion", TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    /// <summary>
    /// Columna que representa el borrado lógico del registro.
    /// </summary>
    public bool Estado { get; set; }
}
