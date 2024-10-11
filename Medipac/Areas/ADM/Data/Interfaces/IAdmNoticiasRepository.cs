using Medipac.Models;

namespace Medipac.Data.ADM.Interfaces
{
    public interface IAdmNoticiasRepository
    {
        Task<List<AdmNoticias>> GetAll();
        Task<AdmNoticias> GetById(int id);
        Task<AdmNoticias> Add(AdmNoticias admNoticias);
        void Update(AdmNoticias admNoticias);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}