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
        public string? Descripcion { get; set; }

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