using Medipac.Models;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Areas.RES.Data.Interfaces
{
    public interface IResAgendaRepository
    {
        Task<List<ResAgenda>> GetAll();
        Task<ResAgenda> GetById(int id);
        Task<ResAgenda> Add(ResAgenda resagenda);
        void Update(ResAgenda resagenda);
        Task<bool> DeleteById(int id);
        Task<int> Save();
        Task<bool> ExisteConflictoHorario(int idMedico, DateOnly fecha, int horaInicio, int horaFin, int? idExcluir = null);
        Task<IEnumerable<ResAgenda>> GetDisponibilidadByMedicoAndDateRange(int medicoId);

    }
}