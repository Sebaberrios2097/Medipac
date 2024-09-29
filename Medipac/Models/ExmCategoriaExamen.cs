using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Models;

[Table("EXM_Categoria_Examen")]
public partial class ExmCategoriaExamen
{
    [Key]
    [Column("Id_Categoria_Examen")]
    public int IdCategoriaExamen { get; set; }

    [Column("Nombre_Categoria")]
    [StringLength(100)]
    public string NombreCategoria { get; set; } = null!;

    public bool Estado { get; set; }

    [InverseProperty("IdCategoriaExamenNavigation")]
    public virtual ICollection<ExmTipoExamen> ExmTipoExamen { get; set; } = new List<ExmTipoExamen>();
}
