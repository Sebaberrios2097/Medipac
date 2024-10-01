using Medipac.Models;
using Medipac.Context;
using Medipac.Data.Interfaces;
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
            db.ComEstadosUsuario.Add(comestadosusuario);
            await Save();
            return comestadosusuario;
        }

        public void Update(ComEstadosUsuario comestadosusuario)
        {
            db.Entry(comestadosusuario).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var comestadosusuario = await GetById(id);
            if (comestadosusuario == null) return false;
            db.ComEstadosUsuario.Remove(comestadosusuario);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}