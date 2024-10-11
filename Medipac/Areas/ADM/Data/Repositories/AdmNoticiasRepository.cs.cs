using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;


namespace Medipac.Data.ADM.Interfaces
{
    public class AdmNoticiasRepository : IAdmNoticiasRepository
    {
        public readonly DbMedipac db;
        public AdmNoticiasRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<AdmNoticias>> GetAll()
        {
            return await db.AdmNoticias.ToListAsync();
        }

        public async Task<AdmNoticias> GetById(int id) => await db.AdmNoticias.FirstOrDefaultAsync(a => a.IdNoticia == id) ?? new AdmNoticias();

        public async Task<AdmNoticias> Add(AdmNoticias admNoticias)
        {
            await db.AdmNoticias.AddAsync(admNoticias);
            return admNoticias;
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.AdmNoticias.FindAsync(id);
            if (entity != null)
            {
                db.AdmNoticias.Remove(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Update(AdmNoticias admNoticias) { db.AdmNoticias.Update(admNoticias); }

        public Task<int> Save() { return db.SaveChangesAsync(); }

    }
}