using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Areas.CLI.Data.DTO
{
    public class DtoCliExamenesSolicitados
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
        public bool Estado { get; set; }
    }
}