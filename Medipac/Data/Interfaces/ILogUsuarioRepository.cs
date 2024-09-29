using Medipac.Models;

namespace Medipac.Data.Interfaces
{
    public interface ILogUsuarioRepository
    {
        Task<List<LogUsuario>> GetAll();
        Task<LogUsuario> GetById(int id);
        Task<LogUsuario> Add(LogUsuario logusuario);
        void Update(LogUsuario logusuario);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}