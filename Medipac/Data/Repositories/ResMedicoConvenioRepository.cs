using Medipac.Models;
using Medipac.Context;
using Medipac.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class ResMedicoConvenioRepository(DbMedipac db) : IResMedicoConvenioRepository
    {
        public readonly DbMedipac db = db;

        public async Task<List<ResMedicoConvenio>> GetAll()
        {
            return await db.ResMedicoConvenio.ToListAsync();
        }

        public async Task<ResMedicoConvenio> GetById(int id) => await db.ResMedicoConvenio
            .FirstOrDefaultAsync(a => a.IdMedicoConvenio == id) ?? new ResMedicoConvenio();

        public async Task<ResMedicoConvenio> Add(ResMedicoConvenio resmedicoconvenio)
        {
            db.ResMedicoConvenio.Add(resmedicoconvenio);
            await Save();
            return resmedicoconvenio;
        }

        public void Update(ResMedicoConvenio resmedicoconvenio)
        {
            db.Entry(resmedicoconvenio).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var resmedicoconvenio = await GetById(id);
            if (resmedicoconvenio == null) return false;
            db.ResMedicoConvenio.Remove(resmedicoconvenio);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}