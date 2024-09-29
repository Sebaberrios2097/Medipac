using Medipac.Models;

namespace Medipac.Data.Interfaces
{
    public interface ICliHistorialPacienteRepository
    {
        Task<List<CliHistorialPaciente>> GetAll();
        Task<CliHistorialPaciente> GetById(int id);
        Task<CliHistorialPaciente> Add(CliHistorialPaciente clihistorialpaciente);
        void Update(CliHistorialPaciente clihistorialpaciente);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}