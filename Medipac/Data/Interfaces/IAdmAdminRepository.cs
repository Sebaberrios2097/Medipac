using Medipac.Models;

namespace Medipac.Data.Interfaces
{
    public interface IAdmAdminRepository
    {
        Task<List<AdmAdmin>> GetAll();
        Task<AdmAdmin> GetById(int id);
        Task<AdmAdmin> Add(AdmAdmin admadmin);
        void Update(AdmAdmin admadmin);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}