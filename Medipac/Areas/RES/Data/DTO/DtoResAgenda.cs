using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Areas.RES.Data.DTO
{
    public class DtoResAgenda
    {
        [Key]
        public int IdAgenda { get; set; }
        public int IdMedico { get; set; }
        public DateOnly Fecha { get; set; }
        public int HoraInicio { get; set; }
        public int HoraFin { get; set; }
        public bool Disponible { get; set; }
        public string? Descripcion { get; set; }

        public List<DayOfWeek>? DiasRecurrentes { get; set; }
        public DateOnly FechaFinRecurrente { get; set; }

        // Propiedades para la vista en formato "HH:mm"
        [NotMapped]
        public string HoraInicioDisplay
        {
            get => $"{HoraInicio / 100:D2}:{HoraInicio % 100:D2}";
            set
            {
                if (TimeSpan.TryParse(value, out var time))
                {
                    HoraInicio = time.Hours * 100 + time.Minutes;
                }
            }
        }

        [NotMapped]
        public string HoraFinDisplay
        {
            get => $"{HoraFin / 100:D2}:{HoraFin % 100:D2}";
            set
            {
                if (TimeSpan.TryParse(value, out var time))
                {
                    HoraFin = time.Hours * 100 + time.Minutes;
                }
            }
        }
    }
}