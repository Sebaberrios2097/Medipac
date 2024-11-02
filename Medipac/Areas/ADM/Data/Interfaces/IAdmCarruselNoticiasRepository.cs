using Medipac.Models;

namespace Medipac.Data.ADM.Interfaces
{
    public interface IAdmCarruselNoticiasRepository
    {
        Task<List<AdmCarruselNoticias>> GetAll();
        Task<List<AdmCarruselNoticias>> GetAllActive();
        Task<AdmCarruselNoticias> GetById(int id);
        Task<AdmCarruselNoticias> Add(AdmCarruselNoticias admCarruselNoticias);
        void Update(AdmCarruselNoticias admCarruselNoticias);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}