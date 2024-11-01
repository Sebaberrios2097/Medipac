using Medipac.Models;

namespace Medipac.Areas.CLI.Data.Interfaces
{
    public interface ICliMedicoRepository
    {
        Task<List<CliMedico>> GetAll();
        Task<CliMedico> GetById(int id);
        Task<CliMedico> GetByUserId(string id);
        Task<CliMedico> Add(CliMedico climedico);
        void Update(CliMedico climedico);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}