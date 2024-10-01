using Medipac.Models;
using Medipac.Context;
using Medipac.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class AdmCarruselNoticiasRepository : IAdmCarruselNoticiasRepository
    {
        private readonly DbMedipac db;

        public AdmCarruselNoticiasRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<AdmCarruselNoticias>> GetAll()
        {
            return await db.AdmCarruselNoticias.ToListAsync();
        }

        public async Task<AdmCarruselNoticias> GetById(int id) => await db.AdmCarruselNoticias
            .FirstOrDefaultAsync(a => a.IdCarruselNoticias == id) ?? new AdmCarruselNoticias();

        public async Task<AdmCarruselNoticias> Add(AdmCarruselNoticias admcarruselnoticias)
        {
            db.AdmCarruselNoticias.Add(admcarruselnoticias);
            await Save();
            return admcarruselnoticias;
        }

        public void Update(AdmCarruselNoticias admcarruselnoticias)
        {
            db.Entry(admcarruselnoticias).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var admcarruselnoticias = await GetById(id);
            if (admcarruselnoticias == null) return false;
            db.AdmCarruselNoticias.Remove(admcarruselnoticias);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}