using Medipac.Models;

namespace Medipac.Data.Interfaces
{
    public interface IResReservaRepository
    {
        Task<List<ResReserva>> GetAll();
        Task<ResReserva> GetById(int id);
        Task<ResReserva> Add(ResReserva resreserva);
        void Update(ResReserva resreserva);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}