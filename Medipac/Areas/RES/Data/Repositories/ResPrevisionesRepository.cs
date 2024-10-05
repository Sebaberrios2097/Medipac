using Medipac.Areas.RES.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class ResPrevisionesRepository : IResPrevisionesRepository
    {
        private readonly DbMedipac db;

        public ResPrevisionesRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<ResPrevisiones>> GetAll()
        {
            return await db.ResPrevisiones.ToListAsync();
        }

        public async Task<ResPrevisiones> GetById(int id) => await db.ResPrevisiones
            .FirstOrDefaultAsync(a => a.IdPrevision == id) ?? new ResPrevisiones();

        public async Task<ResPrevisiones> Add(ResPrevisiones resprevisiones)
        {
            _ = await db.ResPrevisiones.AddAsync(resprevisiones);
            return resprevisiones;
        }

        public void Update(ResPrevisiones resprevisiones)
        {
            _ = db.ResPrevisiones.Update(resprevisiones);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.ResPrevisiones.FindAsync(id);
            if (entity != null)
            {
                _ = db.ResPrevisiones.Remove(entity);
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