using Medipac.Models;

namespace Medipac.Areas.CLI.Data.Interfaces
{
    public interface ICliPacientesRepository
    {
        Task<List<CliPacientes>> GetAll();
        Task<CliPacientes> GetById(int id);
        Task<CliPacientes> Add(CliPacientes clipacientes);
        void Update(CliPacientes clipacientes);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}