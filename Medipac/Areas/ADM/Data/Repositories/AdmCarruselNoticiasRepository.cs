using Medipac.Areas.ADM.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
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
            _ = await db.AdmCarruselNoticias.AddAsync(admcarruselnoticias);
            return admcarruselnoticias;
        }

        public void Update(AdmCarruselNoticias admcarruselnoticias)
        {
            _ = db.AdmCarruselNoticias.Update(admcarruselnoticias);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.AdmCarruselNoticias.FindAsync(id);
            if (entity != null)
            {
                _ = db.AdmCarruselNoticias.Remove(entity);
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