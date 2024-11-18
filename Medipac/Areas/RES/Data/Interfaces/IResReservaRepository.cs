using Medipac.Models;

namespace Medipac.Areas.RES.Data.Interfaces
{
    public interface IResReservaRepository
    {
        Task<List<ResReserva>> GetAll();
        Task<ResReserva> GetById(int id);
        Task<List<ResReserva>> GetByMedicoId(int id);
        Task<List<ResReserva>> GetByPacienteId(int id);
        Task<ResReserva> Add(ResReserva resreserva);
        void Update(ResReserva resreserva);
        Task<bool> DeleteById(int id);
        Task<int> Save();
        Task<IEnumerable<ResEspecialidades>> GetEspecialidades();
        Task<IEnumerable<CliMedico>> GetMedicosByEspecialidad(int idEspecialidad);
    }
}