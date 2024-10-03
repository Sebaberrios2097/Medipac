using Medipac.Models;

namespace Medipac.Areas.RES.Data.Interfaces
{
    public interface IResPrevisionesRepository
    {
        Task<List<ResPrevisiones>> GetAll();
        Task<ResPrevisiones> GetById(int id);
        Task<ResPrevisiones> Add(ResPrevisiones resprevisiones);
        void Update(ResPrevisiones resprevisiones);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}