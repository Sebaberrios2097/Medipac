using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Areas.CLI.Data.DTO
{
    public class DtoCliRecetaPaciente
    {
        [Key]
        [Column("Id_Receta_Paciente")]
        public int IdRecetaPaciente { get; set; }
        [Column("Id_Historial_Paciente")]
        public int IdHistorialPaciente { get; set; }
        [Column(TypeName = "text")]
        public string Receta { get; set; } = null!;
        public bool Estado { get; set; }
    }
}