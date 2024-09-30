using Medipac.Data.DTO;
using Medipac.Models;

namespace Medipac.ReadOnly.DtoTransformation
{
    public static class ExmDtoTransformation
    {
        // Transformación modelo ExmCategoriaExamen
        public static DtoExmCategoriaExamen ToDto(ExmCategoriaExamen original)
        {
            return new()
            {
                IdCategoriaExamen = original.IdCategoriaExamen,
                Estado = original.Estado,
                NombreCategoria = original.NombreCategoria,
            };
        }
        public static ExmCategoriaExamen ToOriginal(DtoExmCategoriaExamen dto)
        {
            return new()
            {
                IdCategoriaExamen = dto.IdCategoriaExamen,
                Estado = dto.Estado,
                NombreCategoria = dto.NombreCategoria,
            };
        }

        // Transformación modelo ExmTipoExamen
        public static DtoExmTipoExamen ToDto(ExmTipoExamen original)
        {
            return new()
            {
                IdCategoriaExamen = original.IdCategoriaExamen,
                IdTipoExamen = original.IdTipoExamen,
                NombreTipoExamen = original.NombreTipoExamen,
                CodigoExamen = original.CodigoExamen,
                TextoAdicional = original.TextoAdicional
            };
        }
        public static ExmTipoExamen ToDto(DtoExmTipoExamen dto)
        {
            return new()
            {
                IdCategoriaExamen = dto.IdCategoriaExamen,
                IdTipoExamen = dto.IdTipoExamen,
                NombreTipoExamen = dto.NombreTipoExamen,
                CodigoExamen = dto.CodigoExamen,
                TextoAdicional = dto.TextoAdicional
            };
        }
    }
}
