using Medipac.Data.ADM.DTO;
using Medipac.Models;

namespace Medipac.ReadOnly.DtoTransformation
{
    public static class AdmDtoTransformation
    {
        // Transformación modelo AdmAdmin
        public static AdmNoticias ToOriginal(this DtoNoticias dto)
        {

            return new()

            {

                IdNoticia = dto.IdNoticia,

                IdUsuario = dto.IdUsuario,

                Titulo = dto.Titulo,

                Subtitulo = dto.Subtitulo,

                Contenido = dto.Contenido,

                FechaPublicacion = dto.FechaPublicacion,

                UrlImagen = dto.UrlImagen,

                Activo = dto.Activo

            };

        }

        public static DtoNoticias ToDto(this AdmNoticias original)
        {

            return new()

            {

                IdNoticia = original.IdNoticia,

                IdUsuario = original.IdUsuario,

                Titulo = original.Titulo,

                Subtitulo = original.Subtitulo,

                Contenido = original.Contenido,

                FechaPublicacion = original.FechaPublicacion,

                UrlImagen = original.UrlImagen,

                Activo = original.Activo

            };

        }

        public static AdmCarruselNoticias ToOriginal(this DtoCarruselNoticias dto)
        {

            return new()

            {

                IdCarruselNoticias = dto.IdCarruselNoticias,

                IdNoticia = dto.IdNoticia,

                Nombre = dto.Nombre,

                Titulo = dto.Titulo,

                Subtitulo = dto.Subtitulo,

                UrlImagen = dto.UrlImagen,

                Activo = dto.Activo

            };

        }

        public static DtoCarruselNoticias ToDto(this AdmCarruselNoticias original)
        {

            return new()

            {

                IdCarruselNoticias = original.IdCarruselNoticias,

                IdNoticia = original.IdNoticia,

                Nombre = original.Nombre,

                Titulo = original.Titulo,

                Subtitulo = original.Subtitulo,

                UrlImagen = original.UrlImagen,

                Activo = original.Activo

            };

        }

    }
}
