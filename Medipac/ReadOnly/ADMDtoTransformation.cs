using Medipac.Data.DTO;
using Medipac.Models;

namespace Medipac.ReadOnly
{
    public static class ADMDtoTransformation
    {
        // Transformación modelo AdmAdmin
        public static DtoAdmAdmin ToDto(this AdmAdmin original)
        {
            return new()
            {
                IdAdmin = original.IdAdmin,
                IdUsuario = original.IdUsuario,
            };
        }

        public static AdmAdmin ToOriginal(this DtoAdmAdmin dto)
        {
            return new()
            {
                IdAdmin = dto.IdAdmin,
                IdUsuario = dto.IdUsuario,
            };
        }

        // Transformación modelo AdmCarruselNoticias
        public static DtoAdmCarruselNoticias ToDto(this AdmCarruselNoticias original)
        {
            return new()
            {
                IdCarruselNoticias = original.IdCarruselNoticias,
                IdNoticia = original.IdNoticia,
                Nombre = original.Nombre,
                Titulo = original.Titulo,
                Subtitulo = original.Subtitulo,
                UrlImagen = original.UrlImagen,
                Activo = original.Activo,
            };
        }
        public static AdmCarruselNoticias ToOriginal(this DtoAdmCarruselNoticias dto)
        {
            return new()
            {
                IdCarruselNoticias = dto.IdCarruselNoticias,
                IdNoticia = dto.IdNoticia,
                Nombre = dto.Nombre,
                Titulo = dto.Titulo,
                Subtitulo = dto.Subtitulo,
                UrlImagen = dto.UrlImagen,
                Activo = dto.Activo,
            };
        }

        // Transformación modelo ADMNoticias
        public static DtoAdmNoticias ToDto(this AdmNoticias original)
        {
            return new()
            {
                IdNoticia = original.IdNoticia,
                IdAdmin = original.IdAdmin,
                Titulo = original.Titulo,
                Subtitulo = original.Subtitulo,
                UrlImagen = original.UrlImagen,
                Contenido = original.Contenido,
                FechaPublicacion = original.FechaPublicacion,
                Activo = original.Activo,
            };
        }

        public static AdmNoticias ToOriginal(this DtoAdmNoticias dto)
        {
            return new()
            {
                IdNoticia = dto.IdNoticia,
                IdAdmin = dto.IdAdmin,
                Titulo = dto.Titulo,
                Subtitulo = dto.Subtitulo,
                UrlImagen = dto.UrlImagen,
                Contenido = dto.Contenido,
                FechaPublicacion = dto.FechaPublicacion,
                Activo = dto.Activo,
            };
        }

    }
}
