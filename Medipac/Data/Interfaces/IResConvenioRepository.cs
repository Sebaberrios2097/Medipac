using Medipac.Models;

namespace Medipac.Data.Interfaces
{
    public interface IResConvenioRepository
    {
        Task<List<ResConvenio>> GetAll();
        Task<ResConvenio> GetById(int id);
        Task<ResConvenio> Add(ResConvenio resconvenio);
        void Update(ResConvenio resconvenio);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}