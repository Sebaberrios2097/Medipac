using Medipac.Areas.RES.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
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
            _ = await db.ResConvenio.AddAsync(resconvenio);
            return resconvenio;
        }

        public void Update(ResConvenio resconvenio)
        {
            _ = db.ResConvenio.Update(resconvenio);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.ResConvenio.FindAsync(id);
            if (entity != null)
            {
                _ = db.ResConvenio.Remove(entity);
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