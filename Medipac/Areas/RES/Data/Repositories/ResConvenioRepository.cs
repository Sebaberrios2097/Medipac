using Medipac.Models;
using Medipac.Context;
using Microsoft.EntityFrameworkCore;
using Medipac.Areas.RES.Data.Interfaces;

namespace Medipac.Areas.RES.Data.Repositories
{
    public class ResConvenioRepository : IResConvenioRepository
    {
        private readonly DbMedipac db;

        public ResConvenioRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<ResConvenio>> GetAll()
        {
            return await db.ResConvenio.ToListAsync();
        }

        public async Task<ResConvenio> GetById(int id) => await db.ResConvenio
            .FirstOrDefaultAsync(a => a.IdConvenio == id) ?? new ResConvenio();

        public async Task<ResConvenio> Add(ResConvenio resconvenio)
        {
            db.ResConvenio.Add(resconvenio);
            await Save();
            return resconvenio;
        }

        public void Update(ResConvenio resconvenio)
        {
            db.Entry(resconvenio).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var resconvenio = await GetById(id);
            if (resconvenio == null) return false;
            db.ResConvenio.Remove(resconvenio);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}