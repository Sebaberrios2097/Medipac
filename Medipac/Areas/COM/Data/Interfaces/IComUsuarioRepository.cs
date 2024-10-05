using Medipac.Areas.COM.Data.DTO;
using Medipac.Models;

namespace Medipac.Areas.COM.Data.Interfaces
{
    public interface IComUsuarioRepository
    {
        Task<List<ComUsuario>> GetAll();
        Task<ComUsuario> GetById(int id);
        Task<ComUsuario> Add(ComUsuario comusuario);
        void Update(ComUsuario comusuario);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}