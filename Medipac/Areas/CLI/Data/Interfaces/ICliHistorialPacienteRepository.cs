using Medipac.Models;
using Microsoft.AspNetCore.Mvc;

namespace Medipac.Areas.CLI.Data.Interfaces
{
    public interface ICliHistorialPacienteRepository
    {
        Task<List<CliHistorialPaciente>> GetAll();
        Task<List<CliHistorialPaciente>> GetHistorialesPorMedico(int idMedico);
        Task<List<CliHistorialPaciente>> GetHistorialesPorPaciente(int idPaciente);
        Task<CliHistorialPaciente> GetById(int id);
        Task<CliHistorialPaciente> GetHistorialByIdMedicoYPaciente(int idMedico, int idPaciente);
        Task<CliHistorialPaciente> Add(CliHistorialPaciente clihistorialpaciente);
        void Update(CliHistorialPaciente clihistorialpaciente);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}