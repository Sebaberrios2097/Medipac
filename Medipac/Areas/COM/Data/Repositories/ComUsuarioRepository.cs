using Medipac.Areas.COM.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class ComUsuarioRepository : IComUsuarioRepository
    {
        private readonly DbMedipac db;

        public ComUsuarioRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<ComUsuario>> GetAll()
        {
            return await db.ComUsuario.ToListAsync();
        }

        public async Task<ComUsuario> GetById(int id) => await db.ComUsuario
            .FirstOrDefaultAsync(a => a.IdUsuario == id) ?? new ComUsuario();

        public async Task<ComUsuario> Add(ComUsuario comusuario)
        {
            _ = await db.ComUsuario.AddAsync(comusuario);
            return comusuario;
        }

        public void Update(ComUsuario comusuario)
        {
            _ = db.ComUsuario.Update(comusuario);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.ComUsuario.FindAsync(id);
            if (entity != null)
            {
                _ = db.ComUsuario.Remove(entity);
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