using Medipac.Areas.RES.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class ResMedicoConvenioRepository : IResMedicoConvenioRepository
    {
        private readonly DbMedipac db;

        public ResMedicoConvenioRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<ResMedicoConvenio>> GetAll()
        {
            return await db.ResMedicoConvenio.ToListAsync();
        }

        public async Task<ResMedicoConvenio> GetById(int id) => await db.ResMedicoConvenio
            .FirstOrDefaultAsync(a => a.IdMedicoConvenio == id) ?? new ResMedicoConvenio();

        public async Task<ResMedicoConvenio> Add(ResMedicoConvenio resmedicoconvenio)
        {
            _ = await db.ResMedicoConvenio.AddAsync(resmedicoconvenio);
            return resmedicoconvenio;
        }

        public void Update(ResMedicoConvenio resmedicoconvenio)
        {
            _ = db.ResMedicoConvenio.Update(resmedicoconvenio);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.ResMedicoConvenio.FindAsync(id);
            if (entity != null)
            {
                _ = db.ResMedicoConvenio.Remove(entity);
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