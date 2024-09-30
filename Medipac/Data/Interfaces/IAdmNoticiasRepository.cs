using Medipac.Models;

namespace Medipac.Data.Interfaces
{
    public interface IAdmNoticiasRepository
    {
        Task<List<AdmNoticias>> GetAll();
        Task<AdmNoticias> GetById(int id);
        Task<AdmNoticias> Add(AdmNoticias admnoticias);
        void Update(AdmNoticias admnoticias);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}