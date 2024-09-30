using Medipac.Data.DTO;
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
                Fecha = dto.Fecha,
                HoraFin = dto.HoraFin,
                HoraInicio = dto.HoraInicio,
                IdAgenda = dto.IdAgenda,
                IdMedico = dto.IdMedico
            };
        }

        // Transformación modelo ResCentroMedico
        public static DtoResCentroMedico ToDto(this ResCentroMedico original)
        {
            return new()
            {
                Correo = original.Correo,
                Direccion = original.Direccion,
                Estado = original.Estado,
                IdCentroMedico = original.IdCentroMedico,
                Nombre = original.Nombre,
                Telefono = original.Telefono
            };
        }

        public static ResCentroMedico ToOriginal(this DtoResCentroMedico dto)
        {
            return new()
            {
                Correo = dto.Correo,
                Direccion = dto.Direccion,
                Estado = dto.Estado,
                IdCentroMedico = dto.IdCentroMedico,
                Nombre = dto.Nombre,
                Telefono = dto.Telefono
            };
        }

        // Transformación modelo ResConvenio
        public static DtoResConvenio ToDto(this ResConvenio original) 
        {
            return new()
            {
                Estado = original.Estado,
                IdConvenio = original.IdConvenio,
                Nombre = original.Nombre,
                Tipo = original.Tipo
            };
        }

        public static ResConvenio ToOriginal(this DtoResConvenio dto)
        {
            return new()
            {
                Estado = dto.Estado,
                IdConvenio = dto.IdConvenio,
                Nombre= dto.Nombre,
                Tipo = dto.Tipo
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
        // Transformación modelo ResMedicoCentroMedico
        public static DtoResMedicoCentroMedico ToDto(this ResMedicoCentroMedico original)
        {
            return new()
            {
                IdCentroMedico = original.IdCentroMedico,
                IdMedico = original.IdMedico,
                IdMedicoCentroMedico = original.IdMedicoCentroMedico
            };
        }

        public static ResMedicoCentroMedico ToOriginal(this DtoResMedicoCentroMedico dto)
        {
            return new()
            {
                IdCentroMedico = dto.IdCentroMedico,
                IdMedico = dto.IdMedico,
                IdMedicoCentroMedico = dto.IdMedicoCentroMedico
            };
        }
        // Transformación modelo ResMedicoConvenio
        public static DtoResMedicoConvenio ToDto(this ResMedicoConvenio original)
        {
            return new()
            {
                IdConvenio = original.IdConvenio,
                IdMedico = original.IdMedico,
                IdMedicoConvenio = original.IdConvenio
            };
        }

        public static ResMedicoConvenio ToOriginal(this DtoResMedicoConvenio dto)
        {
            return new()
            {
                IdConvenio = dto.IdConvenio,
                IdMedico = dto.IdMedico,
                IdMedicoConvenio = dto.IdMedicoConvenio
            };
        }

        // Transformación modelo ResMedicoEspecialidad
        public static DtoResMedicoEspecialidad ToDto(this ResMedicoEspecialidad original)
        {
            return new()
            {
                IdEspecialidad = original.IdEspecialidad,
                IdMedico = original.IdMedico,
                IdMedicoEspecialidad = original.IdMedicoEspecialidad,
            };
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
        // Transformación modelo ResPrevisiones
        public static DtoResPrevisiones ToDto(this ResPrevisiones original)
        {
            return new()
            {
                Estado = original.Estado,
                IdPrevision = original.IdPrevision,
                Nombre = original.Nombre
            };
        }

        public static ResPrevisiones ToOriginal(this DtoResPrevisiones dto)
        {
            return new()
            {
                Estado = dto.Estado,
                IdPrevision = dto.IdPrevision,
                Nombre = dto.Nombre
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
                IdCentroMedico = original.IdCentroMedico,
                IdMedico = original.IdMedico,
                IdPaciente = original.IdPaciente,
                IdPrevision = original.IdPrevision,
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
                IdCentroMedico = dto.IdCentroMedico,
                IdMedico = dto.IdMedico,
                IdPaciente = dto.IdPaciente,
                IdPrevision = dto.IdPrevision,
                IdReserva = dto.IdReserva
            };
        }

    }
}
