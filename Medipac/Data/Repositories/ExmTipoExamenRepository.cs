using Medipac.Models;
using Medipac.Context;
using Medipac.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class ExmTipoExamenRepository : IExmTipoExamenRepository
    {
        private readonly DbMedipac db;

        public ExmTipoExamenRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<ExmTipoExamen>> GetAll()
        {
            return await db.ExmTipoExamen.ToListAsync();
        }

        public async Task<ExmTipoExamen> GetById(int id) => await db.ExmTipoExamen
            .FirstOrDefaultAsync(a => a.IdTipoExamen == id) ?? new ExmTipoExamen();

        public async Task<ExmTipoExamen> Add(ExmTipoExamen exmtipoexamen)
        {
            db.ExmTipoExamen.Add(exmtipoexamen);
            await Save();
            return exmtipoexamen;
        }

        public void Update(ExmTipoExamen exmtipoexamen)
        {
            db.Entry(exmtipoexamen).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var exmtipoexamen = await GetById(id);
            if (exmtipoexamen == null) return false;
            db.ExmTipoExamen.Remove(exmtipoexamen);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}