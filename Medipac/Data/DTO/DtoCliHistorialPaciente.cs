using Medipac.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Data.DTO
{
    public class DtoCliHistorialPaciente
    {
        [Key]
        [Column("Id_Historial_Paciente")]
        public int IdHistorialPaciente { get; set; }
        [Column("Id_Paciente")]
        public int IdPaciente { get; set; }
        [Column("Id_Medico")]
        public int IdMedico { get; set; }
        [Column(TypeName = "text")]
        public string Historial { get; set; } = null!;
        [Column("Fecha_Creacion", TypeName = "datetime")]
        public DateTime FechaCreacion { get; set; }
        [Column("Fecha_Historial", TypeName = "datetime")]
        public DateTime FechaHistorial { get; set; }
        public bool Estado { get; set; }

        internal CliHistorialPaciente ToOriginal()
        {
            throw new NotImplementedException();
        }
    }
}