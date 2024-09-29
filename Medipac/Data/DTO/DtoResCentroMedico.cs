using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medipac.Data.DTO
{
    public class DtoResCentroMedico
    {
        [Key]
        [Column("Id_Centro_Medico")]
        public int IdCentroMedico { get; set; }
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        [StringLength(250)]
        public string Direccion { get; set; } = null!;
        [StringLength(20)]
        public string? Telefono { get; set; }
        [StringLength(100)]
        public string? Correo { get; set; }
        public bool Estado { get; set; }
    }
}