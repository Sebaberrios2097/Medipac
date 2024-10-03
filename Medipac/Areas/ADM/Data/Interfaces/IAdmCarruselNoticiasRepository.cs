using Medipac.Models;

namespace Medipac.Areas.ADM.Data.Interfaces
{
    public interface IAdmCarruselNoticiasRepository
    {
        Task<List<AdmCarruselNoticias>> GetAll();
        Task<AdmCarruselNoticias> GetById(int id);
        Task<AdmCarruselNoticias> Add(AdmCarruselNoticias admcarruselnoticias);
        void Update(AdmCarruselNoticias admcarruselnoticias);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}