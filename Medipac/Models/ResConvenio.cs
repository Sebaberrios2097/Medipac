using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Models;

[Table("RES_Convenio")]
public partial class ResConvenio
{
    [Key]
    [Column("Id_Convenio")]
    public int IdConvenio { get; set; }

    [StringLength(50)]
    public string Nombre { get; set; } = null!;

    [StringLength(25)]
    public string Tipo { get; set; } = null!;

    /// <summary>
    /// Columna que representa el borrado lógico del registro.
    /// </summary>
    public bool Estado { get; set; }

    [InverseProperty("IdConvenioNavigation")]
    public virtual ICollection<ResMedicoConvenio> ResMedicoConvenio { get; set; } = new List<ResMedicoConvenio>();
}
