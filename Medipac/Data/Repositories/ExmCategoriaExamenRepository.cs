using Medipac.Models;
using Medipac.Context;
using Medipac.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class ExmCategoriaExamenRepository(DbMedipac db) : IExmCategoriaExamenRepository
    {
        public readonly DbMedipac db = db;

        public async Task<List<ExmCategoriaExamen>> GetAll()
        {
            return await db.ExmCategoriaExamen.ToListAsync();
        }

        public async Task<ExmCategoriaExamen> GetById(int id) => await db.ExmCategoriaExamen
            .FirstOrDefaultAsync(a => a.IdCategoriaExamen == id) ?? new ExmCategoriaExamen();

        public async Task<ExmCategoriaExamen> Add(ExmCategoriaExamen exmcategoriaexamen)
        {
            db.ExmCategoriaExamen.Add(exmcategoriaexamen);
            await Save();
            return exmcategoriaexamen;
        }

        public void Update(ExmCategoriaExamen exmcategoriaexamen)
        {
            db.Entry(exmcategoriaexamen).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var exmcategoriaexamen = await GetById(id);
            if (exmcategoriaexamen == null) return false;
            db.ExmCategoriaExamen.Remove(exmcategoriaexamen);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}