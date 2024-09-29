using Medipac.Models;

namespace Medipac.Data.Interfaces
{
    public interface IResMedicoCentroMedicoRepository
    {
        Task<List<ResMedicoCentroMedico>> GetAll();
        Task<ResMedicoCentroMedico> GetById(int id);
        Task<ResMedicoCentroMedico> Add(ResMedicoCentroMedico resmedicocentromedico);
        void Update(ResMedicoCentroMedico resmedicocentromedico);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}