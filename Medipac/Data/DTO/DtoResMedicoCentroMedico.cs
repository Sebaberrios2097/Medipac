using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Data.DTO
{
    public class DtoResMedicoCentroMedico
    {
        [Key]
        [Column("Id_Medico_Centro_Medico")]
        public int IdMedicoCentroMedico { get; set; }
        [Column("Id_Medico")]
        public int IdMedico { get; set; }
        [Column("Id_Centro_Medico")]
        public int IdCentroMedico { get; set; }
    }
}