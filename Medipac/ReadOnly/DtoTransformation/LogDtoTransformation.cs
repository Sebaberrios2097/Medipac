using Medipac.Areas.COM.Data.DTO;
using Medipac.Models;

namespace Medipac.ReadOnly.DtoTransformation
{
    public static class LogDtoTransformation
    {
        // Transformación modelo LogUsuario
        public static DtoLogUsuario ToDto(this LogUsuario original) 
        {

            return new()
            {
                Exitoso = original.Exitoso,
                FechaCreacion = original.FechaCreacion,
                IdLogUsuario = original.IdLogUsuario,
                IdUsuario = original.IdUsuario
            };
        }
        public static LogUsuario ToOriginal(this DtoLogUsuario dto)
        {

            return new()
            {
                Exitoso = dto.Exitoso,
                FechaCreacion = dto.FechaCreacion,
                IdLogUsuario = dto.IdLogUsuario,
                IdUsuario = dto.IdUsuario
            };

        }

    }
}
