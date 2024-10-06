using Medipac.Areas.CLI.Data.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Areas.RES.Data.DTO
{
    public class DtoResMedicoEspecialidad
    {
        [Key]
        [Column("Id_Medico_Especialidad")]
        public int IdMedicoEspecialidad { get; set; }
        [Column("Id_Medico")]
        public int IdMedico { get; set; }
        [Column("Id_Especialidad")]
        public int IdEspecialidad { get; set; }

        public DtoResEspecialidades? DtoResEspecialidades { get; set; }
        public string? NombreEspecialidad { get; set; }
        public DtoCliMedico? DtoCliMedico { get; set; }
        public string? NombreMedico { get; set; }
    }
}