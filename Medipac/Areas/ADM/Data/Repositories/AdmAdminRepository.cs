using Medipac.Areas.ADM.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
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
            _ = await db.AdmAdmin.AddAsync(admadmin);
            return admadmin;
        }

        public void Update(AdmAdmin admadmin)
        {
            _ = db.AdmAdmin.Update(admadmin);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.AdmAdmin.FindAsync(id);
            if (entity != null)
            {
                _ = db.AdmAdmin.Remove(entity);
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