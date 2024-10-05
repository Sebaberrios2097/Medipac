using Medipac.Areas.ADM.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
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
            _ = await db.AdmNoticias.AddAsync(admnoticias);
            return admnoticias;
        }

        public void Update(AdmNoticias admnoticias)
        {
            _ = db.AdmNoticias.Update(admnoticias);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.AdmNoticias.FindAsync(id);
            if (entity != null)
            {
                _ = db.AdmNoticias.Remove(entity);
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