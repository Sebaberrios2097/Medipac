using Medipac.Models;

namespace Medipac.Areas.RES.Data.Interfaces
{
    public interface IResHorarioMedicoRepository
    {
        Task<List<ResHorarioMedico>> GetAll();
        Task<ResHorarioMedico> GetById(int id);
        Task<List<ResHorarioMedico>> GetByMedicoId(int id);
        Task<ResHorarioMedico> Add(ResHorarioMedico resHorarioMedico);
        void Update(ResHorarioMedico resHorarioMedico);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}