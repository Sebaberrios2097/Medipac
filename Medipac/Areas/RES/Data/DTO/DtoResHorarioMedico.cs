using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Medipac.Areas.RES.Data.DTO
{
    public class DtoResHorarioMedico
    {
        [Key]
        public int IdHorario { get; set; }

        public int IdMedico { get; set; }

        public int DiaSemana { get; set; }

        [DisplayName("Hora de Inicio")]
        public int HoraInicio { get; set; }
        
        [DisplayName("Hora de Término")]

        public int HoraFin { get; set; }

        [NotMapped]
        public string? HoraInicioDisplay { get; set; }

        [NotMapped]
        public string? HoraFinDisplay { get; set; }
    }
}
