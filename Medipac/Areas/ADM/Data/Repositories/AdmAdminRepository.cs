using Medipac.Models;
using Medipac.Context;
using Microsoft.EntityFrameworkCore;
using Medipac.Areas.ADM.Data.Interfaces;

namespace Medipac.Areas.ADM.Data.Repositories
{
    public class AdmAdminRepository : IAdmAdminRepository
    {
        private readonly DbMedipac db;

        public AdmAdminRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<AdmAdmin>> GetAll()
        {
            return await db.AdmAdmin.ToListAsync();
        }

        public async Task<AdmAdmin> GetById(int id) => await db.AdmAdmin
            .FirstOrDefaultAsync(a => a.IdAdmin == id) ?? new AdmAdmin();

        public async Task<AdmAdmin> Add(AdmAdmin admadmin)
        {
            db.AdmAdmin.Add(admadmin);
            await Save();
            return admadmin;
        }

        public void Update(AdmAdmin admadmin)
        {
            db.Entry(admadmin).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var admadmin = await GetById(id);
            if (admadmin == null) return false;
            db.AdmAdmin.Remove(admadmin);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}