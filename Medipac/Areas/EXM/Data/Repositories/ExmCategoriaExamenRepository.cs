using Medipac.Areas.EXM.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class ExmCategoriaExamenRepository : IExmCategoriaExamenRepository
    {
        private readonly DbMedipac db;

        public ExmCategoriaExamenRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<ExmCategoriaExamen>> GetAll()
        {
            return await db.ExmCategoriaExamen.ToListAsync();
        }

        public async Task<ExmCategoriaExamen> GetById(int id) => await db.ExmCategoriaExamen
            .FirstOrDefaultAsync(a => a.IdCategoriaExamen == id) ?? new ExmCategoriaExamen();

        public async Task<ExmCategoriaExamen> Add(ExmCategoriaExamen exmcategoriaexamen)
        {
            _ = await db.ExmCategoriaExamen.AddAsync(exmcategoriaexamen);
            return exmcategoriaexamen;
        }

        public void Update(ExmCategoriaExamen exmcategoriaexamen)
        {
            _ = db.ExmCategoriaExamen.Update(exmcategoriaexamen);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.ExmCategoriaExamen.FindAsync(id);
            if (entity != null)
            {
                _ = db.ExmCategoriaExamen.Remove(entity);
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