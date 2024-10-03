using Medipac.Areas.CLI.Data.DTO;
using Medipac.Models;

namespace Medipac.ReadOnly.DtoTransformation
{
    public static class CliDtoTransformation
    {
        // Conversión modelo CliExamenesSolicitados
        public static DtoCliExamenesSolicitados ToDto(this CliExamenesSolicitados original)
        {
            return new()
            {
                IdExamenesSolicitados = original.IdExamenesSolicitados,
                IdSolicitudExamen = original.IdSolicitudExamen,
                IdTipoExamen = original.IdTipoExamen,
                Descripcion = original.Descripcion,
                FechaCreacion = original.FechaCreacion,
                Estado = original.Estado,
            };
        }
        public static CliExamenesSolicitados ToOriginal(this DtoCliExamenesSolicitados dto)
        {
            return new()
            {
                IdExamenesSolicitados = dto.IdExamenesSolicitados,
                IdSolicitudExamen = dto.IdSolicitudExamen,
                IdTipoExamen = dto.IdTipoExamen,
                Descripcion = dto.Descripcion,
                FechaCreacion = dto.FechaCreacion,
                Estado = dto.Estado,
            };
        }

        // Conversión modelo CliHistorialPaciente
        public static DtoCliHistorialPaciente ToDto(this CliHistorialPaciente original)
        {
            return new()
            {
                IdHistorialPaciente = original.IdHistorialPaciente,
                IdMedico = original.IdMedico,
                IdPaciente = original.IdPaciente,
                FechaCreacion = original.FechaCreacion,
                Estado = original.Estado,
                FechaHistorial = original.FechaHistorial,
                Historial = original.Historial,
            };
        }
        public static CliHistorialPaciente ToDto(this DtoCliHistorialPaciente dto)
        {
            return new()
            {
                IdHistorialPaciente = dto.IdHistorialPaciente,
                IdMedico = dto.IdMedico,
                IdPaciente = dto.IdPaciente,
                FechaCreacion = dto.FechaCreacion,
                Estado = dto.Estado,
                FechaHistorial = dto.FechaHistorial,
                Historial = dto.Historial,
            };
        }

        // Conversión modelo CliMedico
        public static DtoCliMedico ToDto(this CliMedico original)
        {
            return new()
            {
                IdMedico = original.IdMedico,
                IdUsuario = original.IdUsuario,
                Nombres = original.Nombres,
                ApPaterno = original.ApPaterno,
                ApMaterno = original.ApMaterno,
                Correo = original.Correo,
                Fono = original.Fono,
                Estado = original.Estado
            };
        }
        public static CliMedico ToOriginal(this DtoCliMedico dto)
        {
            return new()
            {
                IdMedico = dto.IdMedico,
                IdUsuario = dto.IdUsuario,
                Nombres = dto.Nombres,
                ApPaterno = dto.ApPaterno,
                ApMaterno = dto.ApMaterno,
                Correo = dto.Correo,
                Fono = dto.Fono,
                Estado = dto.Estado
            };
        }

        // Conversión modelo CliPacientes
        public static DtoCliPacientes ToDto(this CliPacientes original)
        {
            return new()
            {
                IdPaciente = original.IdPaciente,
                IdUsuario = original.IdUsuario,
                Nombres = original.Nombres,
                ApPaterno = original.Correo,
                ApMaterno = original.ApMaterno,
                Correo = original.Correo,
                Fono = original.Fono,
                Rut = original.Rut,
                Estado = original.Estado
            };
        }

        public static CliPacientes ToOriginal(this DtoCliPacientes dto)
        {
            return new()
            {
                IdPaciente = dto.IdPaciente,
                IdUsuario = dto.IdUsuario,
                Nombres = dto.Nombres,
                ApPaterno = dto.Correo,
                ApMaterno = dto.ApMaterno,
                Correo = dto.Correo,
                Fono = dto.Fono,
                Rut = dto.Rut,
                Estado = dto.Estado
            };
        }

        // Conversión modelo CliRecetaPaciente
        public static DtoCliRecetaPaciente ToDto(this CliRecetaPaciente original)
        {
            return new()
            {
                IdHistorialPaciente = original.IdHistorialPaciente,
                IdRecetaPaciente = original.IdRecetaPaciente,
                Receta = original.Receta,
                Estado = original.Estado
            };
        }
        public static CliRecetaPaciente ToOriginal(this DtoCliRecetaPaciente dto)
        {
            return new()
            {
                IdHistorialPaciente = dto.IdHistorialPaciente,
                IdRecetaPaciente = dto.IdRecetaPaciente,
                Receta = dto.Receta,
                Estado = dto.Estado
            };
        }

        // Conversión modelo CliSolicitudExamen
        public static DtoCliSolicitudExamen ToDto(this CliSolicitudExamen original)
        {
            return new()
            {
                IdSolicitudExamen = original.IdSolicitudExamen,
                IdHistorialPaciente = original.IdHistorialPaciente,
                FechaCreacion = original.FechaCreacion,
                FechaSolicitud = original.FechaSolicitud,
            };
        }
        public static CliSolicitudExamen ToOriginal(this DtoCliSolicitudExamen dto)
        {
            return new()
            {
                IdSolicitudExamen = dto.IdSolicitudExamen,
                IdHistorialPaciente = dto.IdHistorialPaciente,
                FechaCreacion = dto.FechaCreacion,
                FechaSolicitud = dto.FechaSolicitud,
            };
        }
    }
}
