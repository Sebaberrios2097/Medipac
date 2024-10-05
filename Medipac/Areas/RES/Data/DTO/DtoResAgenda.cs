using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Areas.RES.Data.DTO
{
    public class DtoResAgenda
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
    }
}