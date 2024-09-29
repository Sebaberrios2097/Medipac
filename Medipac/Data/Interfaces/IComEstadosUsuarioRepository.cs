using Medipac.Models;

namespace Medipac.Data.Interfaces
{
    public interface IComEstadosUsuarioRepository
    {
        Task<List<ComEstadosUsuario>> GetAll();
        Task<ComEstadosUsuario> GetById(int id);
        Task<ComEstadosUsuario> Add(ComEstadosUsuario comestadosusuario);
        void Update(ComEstadosUsuario comestadosusuario);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}