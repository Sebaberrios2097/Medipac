using Medipac.Areas.COM.Data.DTO;
using Medipac.Models;

namespace Medipac.ReadOnly.DtoTransformation
{
    public static class ComDtoTransformation
    {
        // Conversión modelo ComEstadosUsuario
        public static DtoComEstadosUsuario ToDto(this ComEstadosUsuario original)
        {
            return new()
            {
                IdEstado = original.IdEstado,
                Descripcion = original.Descripcion,
                Estado = original.Estado,
            };
        }
        public static ComEstadosUsuario ToOriginal(this DtoComEstadosUsuario dto)
        {
            return new()
            {
                IdEstado = dto.IdEstado,
                Descripcion = dto.Descripcion,
                Estado = dto.Estado,
            };
        }

        // Conversión modelo 
        public static DtoComUsuario ToDto(this ComUsuario original)
        {
            return new()
            {
                IdEstado = original.IdEstado,
                IdUsuario = original.IdUsuario,
                Usuario = original.Usuario,
                Password = original.Password,
                FechaCreacion = original.FechaCreacion,
            };
        }

        public static ComUsuario ToOriginal(this DtoComUsuario dto)
        {
            return new()
            {
                IdEstado = dto.IdEstado,
                IdUsuario = dto.IdUsuario,
                Usuario = dto.Usuario,
                Password = dto.Password,
                FechaCreacion = dto.FechaCreacion,
            };
        }
    }
}
