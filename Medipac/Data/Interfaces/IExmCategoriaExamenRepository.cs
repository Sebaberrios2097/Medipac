using Medipac.Models;

namespace Medipac.Data.Interfaces
{
    public interface IExmCategoriaExamenRepository
    {
        Task<List<ExmCategoriaExamen>> GetAll();
        Task<ExmCategoriaExamen> GetById(int id);
        Task<ExmCategoriaExamen> Add(ExmCategoriaExamen exmcategoriaexamen);
        void Update(ExmCategoriaExamen exmcategoriaexamen);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}