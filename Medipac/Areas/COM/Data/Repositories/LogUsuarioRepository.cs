using Medipac.Areas.COM.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class LogUsuarioRepository : ILogUsuarioRepository
    {
        private readonly DbMedipac db;

        public LogUsuarioRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<LogUsuario>> GetAll()
        {
            return await db.LogUsuario.ToListAsync();
        }

        public async Task<LogUsuario> GetById(int id) => await db.LogUsuario
            .FirstOrDefaultAsync(a => a.IdLogUsuario == id) ?? new LogUsuario();

        public async Task<LogUsuario> Add(LogUsuario logusuario)
        {
            _ = await db.LogUsuario.AddAsync(logusuario);
            return logusuario;
        }

        public void Update(LogUsuario logusuario)
        {
            _ = db.LogUsuario.Update(logusuario);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.LogUsuario.FindAsync(id);
            if (entity != null)
            {
                _ = db.LogUsuario.Remove(entity);
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}