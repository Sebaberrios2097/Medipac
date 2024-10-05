using Medipac.Areas.EXM.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
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
            _ = await db.ExmTipoExamen.AddAsync(exmtipoexamen);
            return exmtipoexamen;
        }

        public void Update(ExmTipoExamen exmtipoexamen)
        {
            _ = db.ExmTipoExamen.Update(exmtipoexamen);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.ExmTipoExamen.FindAsync(id);
            if (entity != null)
            {
                _ = db.ExmTipoExamen.Remove(entity);
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