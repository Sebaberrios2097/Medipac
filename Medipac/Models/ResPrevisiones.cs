using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Models;

[Table("RES_Previsiones")]
public partial class ResPrevisiones
{
    [Key]
    [Column("Id_Prevision")]
    public int IdPrevision { get; set; }

    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    /// <summary>
    /// Columna que representa el borrado lógico del registro.
    /// </summary>
    public bool Estado { get; set; }

    [InverseProperty("IdPrevisionNavigation")]
    public virtual ICollection<ResReserva> ResReserva { get; set; } = new List<ResReserva>();
}
