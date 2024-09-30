using Medipac.Models;
using Medipac.Context;
using Medipac.Data.Interfaces;
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
            .FirstOrDefaultAsync(a => a.Id == id) ?? new ComUsuario();

        public async Task<ComUsuario> Add(ComUsuario comusuario)
        {
            db.ComUsuario.Add(comusuario);
            await Save();
            return comusuario;
        }

        public void Update(ComUsuario comusuario)
        {
            db.Entry(comusuario).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var comusuario = await GetById(id);
            if (comusuario == null) return false;
            db.ComUsuario.Remove(comusuario);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}