using Medipac.Models;

namespace Medipac.Data.Interfaces
{
    public interface IExmTipoExamenRepository
    {
        Task<List<ExmTipoExamen>> GetAll();
        Task<ExmTipoExamen> GetById(int id);
        Task<ExmTipoExamen> Add(ExmTipoExamen exmtipoexamen);
        void Update(ExmTipoExamen exmtipoexamen);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}