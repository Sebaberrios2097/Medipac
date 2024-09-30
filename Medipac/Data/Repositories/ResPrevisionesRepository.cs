using Medipac.Models;
using Medipac.Context;
using Medipac.Data.Interfaces;
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
            .FirstOrDefaultAsync(a => a.Id == id) ?? new ResPrevisiones();

        public async Task<ResPrevisiones> Add(ResPrevisiones resprevisiones)
        {
            db.ResPrevisiones.Add(resprevisiones);
            await Save();
            return resprevisiones;
        }

        public void Update(ResPrevisiones resprevisiones)
        {
            db.Entry(resprevisiones).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var resprevisiones = await GetById(id);
            if (resprevisiones == null) return false;
            db.ResPrevisiones.Remove(resprevisiones);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}