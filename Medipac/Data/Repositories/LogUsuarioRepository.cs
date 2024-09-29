using Medipac.Models;
using Medipac.Context;
using Medipac.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class LogUsuarioRepository(DbMedipac db) : ILogUsuarioRepository
    {
        public readonly DbMedipac db = db;

        public async Task<List<LogUsuario>> GetAll()
        {
            return await db.LogUsuario.ToListAsync();
        }

        public async Task<LogUsuario> GetById(int id) => await db.LogUsuario
            .FirstOrDefaultAsync(a => a.IdLogUsuario == id) ?? new LogUsuario();

        public async Task<LogUsuario> Add(LogUsuario logusuario)
        {
            db.LogUsuario.Add(logusuario);
            await Save();
            return logusuario;
        }

        public void Update(LogUsuario logusuario)
        {
            db.Entry(logusuario).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var logusuario = await GetById(id);
            if (logusuario == null) return false;
            db.LogUsuario.Remove(logusuario);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}