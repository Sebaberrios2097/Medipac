using Medipac.Models;
using Medipac.Context;
using Medipac.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class ResAgendaRepository(DbMedipac db) : IResAgendaRepository
    {
        public readonly DbMedipac db = db;

        public async Task<List<ResAgenda>> GetAll()
        {
            return await db.ResAgenda.ToListAsync();
        }

        public async Task<ResAgenda> GetById(int id) => await db.ResAgenda
            .FirstOrDefaultAsync(a => a.IdAgenda == id) ?? new ResAgenda();

        public async Task<ResAgenda> Add(ResAgenda resagenda)
        {
            db.ResAgenda.Add(resagenda);
            await Save();
            return resagenda;
        }

        public void Update(ResAgenda resagenda)
        {
            db.Entry(resagenda).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var resagenda = await GetById(id);
            if (resagenda == null) return false;
            db.ResAgenda.Remove(resagenda);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}