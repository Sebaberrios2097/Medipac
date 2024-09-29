using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Data.DTO
{
    public class DtoResMedicoConvenio
    {
        [Key]
        [Column("Id_Medico_Convenio")]
        public int IdMedicoConvenio { get; set; }
        [Column("Id_Convenio")]
        public int IdConvenio { get; set; }
        [Column("Id_Medico")]
        public int IdMedico { get; set; }
    }
}