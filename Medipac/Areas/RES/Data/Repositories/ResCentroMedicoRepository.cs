using Medipac.Areas.RES.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class ResCentroMedicoRepository : IResCentroMedicoRepository
    {
        private readonly DbMedipac db;

        public ResCentroMedicoRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<ResCentroMedico>> GetAll()
        {
            return await db.ResCentroMedico.ToListAsync();
        }

        public async Task<ResCentroMedico> GetById(int id) => await db.ResCentroMedico
            .FirstOrDefaultAsync(a => a.IdCentroMedico == id) ?? new ResCentroMedico();

        public async Task<ResCentroMedico> Add(ResCentroMedico rescentromedico)
        {
            _ = await db.ResCentroMedico.AddAsync(rescentromedico);
            return rescentromedico;
        }

        public void Update(ResCentroMedico rescentromedico)
        {
            _ = db.ResCentroMedico.Update(rescentromedico);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.ResCentroMedico.FindAsync(id);
            if (entity != null)
            {
                _ = db.ResCentroMedico.Remove(entity);
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