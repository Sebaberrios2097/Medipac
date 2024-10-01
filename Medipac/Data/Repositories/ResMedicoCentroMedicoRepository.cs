using Medipac.Models;
using Medipac.Context;
using Medipac.Data.Interfaces;
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
            db.ResMedicoCentroMedico.Add(resmedicocentromedico);
            await Save();
            return resmedicocentromedico;
        }

        public void Update(ResMedicoCentroMedico resmedicocentromedico)
        {
            db.Entry(resmedicocentromedico).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var resmedicocentromedico = await GetById(id);
            if (resmedicocentromedico == null) return false;
            db.ResMedicoCentroMedico.Remove(resmedicocentromedico);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}