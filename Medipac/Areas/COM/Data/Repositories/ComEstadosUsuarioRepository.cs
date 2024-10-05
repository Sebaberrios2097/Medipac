using Medipac.Areas.COM.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class ComEstadosUsuarioRepository : IComEstadosUsuarioRepository
    {
        private readonly DbMedipac db;

        public ComEstadosUsuarioRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<ComEstadosUsuario>> GetAll()
        {
            return await db.ComEstadosUsuario.ToListAsync();
        }

        public async Task<ComEstadosUsuario> GetById(int id) => await db.ComEstadosUsuario
            .FirstOrDefaultAsync(a => a.IdEstado == id) ?? new ComEstadosUsuario();

        public async Task<ComEstadosUsuario> Add(ComEstadosUsuario comestadosusuario)
        {
            _ = await db.ComEstadosUsuario.AddAsync(comestadosusuario);
            return comestadosusuario;
        }

        public void Update(ComEstadosUsuario comestadosusuario)
        {
            _ = db.ComEstadosUsuario.Update(comestadosusuario);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.ComEstadosUsuario.FindAsync(id);
            if (entity != null)
            {
                _ = db.ComEstadosUsuario.Remove(entity);
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