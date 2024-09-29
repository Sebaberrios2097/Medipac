using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Models;

[Table("EXM_Tipo_Examen")]
public partial class ExmTipoExamen
{
    [Key]
    [Column("Id_Tipo_Examen")]
    public int IdTipoExamen { get; set; }

    [Column("Id_Categoria_Examen")]
    public int IdCategoriaExamen { get; set; }

    [Column("Nombre_Tipo_Examen")]
    [StringLength(500)]
    public string NombreTipoExamen { get; set; } = null!;

    [Column("Codigo_Examen")]
    [StringLength(50)]
    public string CodigoExamen { get; set; } = null!;

    [Column("Texto_Adicional")]
    public bool TextoAdicional { get; set; }

    [InverseProperty("IdTipoExamenNavigation")]
    public virtual ICollection<CliExamenesSolicitados> CliExamenesSolicitados { get; set; } = new List<CliExamenesSolicitados>();

    [ForeignKey("IdCategoriaExamen")]
    [InverseProperty("ExmTipoExamen")]
    public virtual ExmCategoriaExamen IdCategoriaExamenNavigation { get; set; } = null!;
}
