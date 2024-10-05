using Medipac.Areas.RES.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class ResMedicoCentroMedicoRepository : IResMedicoCentroMedicoRepository
    {
        private readonly DbMedipac db;

        public ResMedicoCentroMedicoRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<ResMedicoCentroMedico>> GetAll()
        {
            return await db.ResMedicoCentroMedico.ToListAsync();
        }

        public async Task<ResMedicoCentroMedico> GetById(int id) => await db.ResMedicoCentroMedico
            .FirstOrDefaultAsync(a => a.IdMedicoCentroMedico == id) ?? new ResMedicoCentroMedico();

        public async Task<ResMedicoCentroMedico> Add(ResMedicoCentroMedico resmedicocentromedico)
        {
            _ = await db.ResMedicoCentroMedico.AddAsync(resmedicocentromedico);
            return resmedicocentromedico;
        }

        public void Update(ResMedicoCentroMedico resmedicocentromedico)
        {
            _ = db.ResMedicoCentroMedico.Update(resmedicocentromedico);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.ResMedicoCentroMedico.FindAsync(id);
            if (entity != null)
            {
                _ = db.ResMedicoCentroMedico.Remove(entity);
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