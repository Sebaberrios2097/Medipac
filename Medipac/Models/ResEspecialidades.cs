using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Models;

[Table("RES_Especialidades")]
public partial class ResEspecialidades
{
    [Key]
    [Column("Id_Especialidad")]
    public int IdEspecialidad { get; set; }

    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Columna que representa el borrado lógico del registro.
    /// </summary>
    public bool Estado { get; set; }

    [InverseProperty("IdEspecialidadNavigation")]
    public virtual ICollection<ResMedicoEspecialidad> ResMedicoEspecialidad { get; set; } = new List<ResMedicoEspecialidad>();
}
