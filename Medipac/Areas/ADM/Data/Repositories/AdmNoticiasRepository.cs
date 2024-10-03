using Medipac.Models;
using Medipac.Context;
using Microsoft.EntityFrameworkCore;
using Medipac.Areas.ADM.Data.Interfaces;

namespace Medipac.Areas.ADM.Data.Repositories
{
    public class AdmNoticiasRepository : IAdmNoticiasRepository
    {
        private readonly DbMedipac db;

        public AdmNoticiasRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<AdmNoticias>> GetAll()
        {
            return await db.AdmNoticias.ToListAsync();
        }

        public async Task<AdmNoticias> GetById(int id) => await db.AdmNoticias
            .FirstOrDefaultAsync(a => a.IdNoticia == id) ?? new AdmNoticias();

        public async Task<AdmNoticias> Add(AdmNoticias admnoticias)
        {
            db.AdmNoticias.Add(admnoticias);
            await Save();
            return admnoticias;
        }

        public void Update(AdmNoticias admnoticias)
        {
            db.Entry(admnoticias).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var admnoticias = await GetById(id);
            if (admnoticias == null) return false;
            db.AdmNoticias.Remove(admnoticias);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}