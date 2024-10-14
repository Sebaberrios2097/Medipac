using Medipac.Models;
using System.ComponentModel.DataAnnotations;

namespace Medipac.Areas.CLI.Data.DTO
{
    public class DtoCliMedico
    {
        [Key]
        public int IdMedico { get; set; }
        public int IdUsuario { get; set; }
        public int Rut { get; set; }

        [StringLength(1)]
        public string Dv { get; set; } = null!;
        [StringLength(100)]
        public string Nombres { get; set; } = null!;
        [Display(Name = "Apellido Paterno")]
        [StringLength(50)]
        public string ApPaterno { get; set; } = null!;
        [Display(Name = "Apellido Materno")]
        [StringLength(50)]
        public string? ApMaterno { get; set; }
        [Display(Name = "Teléfono")]
        [StringLength(20)]
        public string Fono { get; set; } = null!;
        [StringLength(100)]
        public string Correo { get; set; } = null!;
        public bool Estado { get; set; }

        public string RutFormateado { get; set; } = null!;

        public List<int> EspecialidadesSeleccionadas { get; set; } = new List<int>();
        public List<string> NombresEspecialidades { get; set; } = new List<string>(); // Nombres de las especialidades
    }
}