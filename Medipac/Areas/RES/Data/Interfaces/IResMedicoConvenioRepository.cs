using Medipac.Models;

namespace Medipac.Areas.RES.Data.Interfaces
{
    public interface IResMedicoConvenioRepository
    {
        Task<List<ResMedicoConvenio>> GetAll();
        Task<ResMedicoConvenio> GetById(int id);
        Task<ResMedicoConvenio> Add(ResMedicoConvenio resmedicoconvenio);
        void Update(ResMedicoConvenio resmedicoconvenio);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}