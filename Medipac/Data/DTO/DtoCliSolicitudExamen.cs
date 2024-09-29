using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Data.DTO
{
    public class DtoCliSolicitudExamen
    {
        [Key]
        [Column("Id_Solicitud_Examen")]
        public int IdSolicitudExamen { get; set; }
        [Column("Id_Historial_Paciente")]
        public int IdHistorialPaciente { get; set; }
        [Column("Fecha_Solicitud", TypeName = "datetime")]
        public DateTime FechaSolicitud { get; set; }
        [Column("Fecha_Creacion", TypeName = "datetime")]
        public DateTime FechaCreacion { get; set; }
    }
}