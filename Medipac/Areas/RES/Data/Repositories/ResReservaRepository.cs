using Medipac.Models;
using Medipac.Context;
using Microsoft.EntityFrameworkCore;
using Medipac.Areas.RES.Data.Interfaces;

namespace Medipac.Areas.RES.Data.Repositories
{
    public class ResReservaRepository : IResReservaRepository
    {
        private readonly DbMedipac db;

        public ResReservaRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<ResReserva>> GetAll()
        {
            return await db.ResReserva.ToListAsync();
        }

        public async Task<ResReserva> GetById(int id) => await db.ResReserva
            .FirstOrDefaultAsync(a => a.IdReserva == id) ?? new ResReserva();

        public async Task<ResReserva> Add(ResReserva resreserva)
        {
            db.ResReserva.Add(resreserva);
            await Save();
            return resreserva;
        }

        public void Update(ResReserva resreserva)
        {
            db.Entry(resreserva).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var resreserva = await GetById(id);
            if (resreserva == null) return false;
            db.ResReserva.Remove(resreserva);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}