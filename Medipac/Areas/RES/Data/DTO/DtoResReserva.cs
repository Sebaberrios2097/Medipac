using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Areas.RES.Data.DTO
{
    public class DtoResReserva
    {
        [Key]
        [Column("Id_Reserva")]
        public int IdReserva { get; set; }
        [Column("Id_Paciente")]
        public int IdPaciente { get; set; }
        [Column("Id_Medico")]
        public int IdMedico { get; set; }
        [Column("Id_Centro_Medico")]
        public int IdCentroMedico { get; set; }
        [Column("Id_Prevision")]
        public int IdPrevision { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
        [Column("Fecha_Creacion", TypeName = "datetime")]
        public DateTime FechaCreacion { get; set; }
    }
}