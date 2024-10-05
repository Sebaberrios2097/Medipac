using Medipac.Models;

namespace Medipac.Areas.RES.Data.Interfaces
{
    public interface IResMedicoEspecialidadRepository
    {
        Task<List<ResMedicoEspecialidad>> GetAll();
        Task<ResMedicoEspecialidad> GetById(int id);
        Task<ResMedicoEspecialidad> Add(ResMedicoEspecialidad resmedicoespecialidad);
        void Update(ResMedicoEspecialidad resmedicoespecialidad);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}