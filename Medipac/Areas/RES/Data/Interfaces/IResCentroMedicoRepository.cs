using Medipac.Models;

namespace Medipac.Areas.RES.Data.Interfaces
{
    public interface IResCentroMedicoRepository
    {
        Task<List<ResCentroMedico>> GetAll();
        Task<ResCentroMedico> GetById(int id);
        Task<ResCentroMedico> Add(ResCentroMedico rescentromedico);
        void Update(ResCentroMedico rescentromedico);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}