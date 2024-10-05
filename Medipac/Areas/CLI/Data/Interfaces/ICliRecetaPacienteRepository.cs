using Medipac.Models;

namespace Medipac.Areas.CLI.Data.Interfaces
{
    public interface ICliRecetaPacienteRepository
    {
        Task<List<CliRecetaPaciente>> GetAll();
        Task<CliRecetaPaciente> GetById(int id);
        Task<CliRecetaPaciente> Add(CliRecetaPaciente clirecetapaciente);
        void Update(CliRecetaPaciente clirecetapaciente);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}