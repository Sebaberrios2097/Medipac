using Medipac.Areas.RES.Data.DTO;
using Medipac.Models;

namespace Medipac.ReadOnly.DtoTransformation
{
    public static class ResDtoTransformation
    {
        // Transformación modelo ResAgenda
        public static DtoResAgenda ToDto(this ResAgenda original)
        {

            return new()
            {
                Disponible = original.Disponible,
                Descripcion = original.Descripcion,
                Fecha = original.Fecha,
                HoraFin = original.HoraFin,
                HoraInicio = original.HoraInicio,
                IdAgenda = original.IdAgenda,
                IdMedico = original.IdMedico
            };

        }

        public static ResAgenda ToOriginal(this DtoResAgenda dto)
        {
            return new()
            {
                Disponible = dto.Disponible,
                Descripcion = dto.Descripcion,
                Fecha = dto.Fecha,
                HoraFin = dto.HoraFin,
                HoraInicio = dto.HoraInicio,
                IdAgenda = dto.IdAgenda,
                IdMedico = dto.IdMedico
            };
        }

        // Transformación modelo ResEspecialidades
        public static DtoResEspecialidades ToDto(this ResEspecialidades original)
        {
            return new()
            {
                Estado = original.Estado,
                IdEspecialidad = original.IdEspecialidad,
                Nombre = original.Nombre
            };
        }

        public static ResEspecialidades ToOriginal(this DtoResEspecialidades dto)
        {
            return new()
            {
                Estado = dto.Estado,
                IdEspecialidad = dto.IdEspecialidad,
                Nombre = dto.Nombre
            };
        }

        // Transformación modelo ResMedicoEspecialidad
        public static DtoResMedicoEspecialidad ToDto(this ResMedicoEspecialidad original)
        {
            var Query = new DtoResMedicoEspecialidad()
            {
                IdEspecialidad = original.IdEspecialidad,
                IdMedico = original.IdMedico,
                IdMedicoEspecialidad = original.IdMedicoEspecialidad,
            };

            if (original.IdMedicoNavigation != null)
            {
                Query.DtoCliMedico = original.IdMedicoNavigation.ToDto();
            }
            if (original.IdEspecialidadNavigation != null)
            {
                Query.DtoResEspecialidades = original.IdEspecialidadNavigation.ToDto();
            }

            return Query;
        }

        public static ResMedicoEspecialidad ToOriginal(this DtoResMedicoEspecialidad dto)
        {
            return new()
            {
                IdEspecialidad = dto.IdEspecialidad,
                IdMedico = dto.IdMedico,
                IdMedicoEspecialidad = dto.IdMedicoEspecialidad
            };
        }
        // Transformación modelo ResReserva
        public static DtoResReserva ToDto(this ResReserva original)
        {
            return new()
            {
                Estado = original.Estado,
                Fecha = original.Fecha,
                FechaCreacion = original.FechaCreacion,
                IdMedico = original.IdMedico,
                IdPaciente = original.IdPaciente,
                IdReserva = original.IdReserva,
            };
        }

        public static ResReserva ToOriginal(this DtoResReserva dto)
        {
            return new()
            {
                Estado = dto.Estado,
                Fecha = dto.Fecha,
                FechaCreacion = dto.FechaCreacion,
                IdMedico = dto.IdMedico,
                IdPaciente = dto.IdPaciente,
                IdReserva = dto.IdReserva
            };
        }

    }
}
