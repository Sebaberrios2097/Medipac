using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;


namespace Medipac.Data.ADM.Interfaces
{
    public class AdmCarruselNoticiasRepository : IAdmCarruselNoticiasRepository
    {
        public readonly DbMedipac db;
        public AdmCarruselNoticiasRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<AdmCarruselNoticias>> GetAll()
        {
            return await db.AdmCarruselNoticias.ToListAsync();
        }
        public async Task<List<AdmCarruselNoticias>> GetAllActive()
        {
            return await db.AdmCarruselNoticias.Where(c => c.Activo).ToListAsync();
        }

        public async Task<AdmCarruselNoticias> GetById(int id) => await db.AdmCarruselNoticias.Include(n => n.IdNoticiaNavigation).FirstOrDefaultAsync(a => a.IdCarruselNoticias == id) ?? new AdmCarruselNoticias();

        public async Task<AdmCarruselNoticias> Add(AdmCarruselNoticias admCarruselNoticias)
        {
            await db.AdmCarruselNoticias.AddAsync(admCarruselNoticias);
            return admCarruselNoticias;
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.AdmCarruselNoticias.FindAsync(id);
            if (entity != null)
            {
                db.AdmCarruselNoticias.Remove(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Update(AdmCarruselNoticias admCarruselNoticias) { db.AdmCarruselNoticias.Update(admCarruselNoticias); }

        public Task<int> Save() { return db.SaveChangesAsync(); }

    }
}