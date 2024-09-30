using Medipac.Models;
using Medipac.Context;
using Medipac.Data.Interfaces;
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
            .FirstOrDefaultAsync(a => a.Id == id) ?? new ResCentroMedico();

        public async Task<ResCentroMedico> Add(ResCentroMedico rescentromedico)
        {
            db.ResCentroMedico.Add(rescentromedico);
            await Save();
            return rescentromedico;
        }

        public void Update(ResCentroMedico rescentromedico)
        {
            db.Entry(rescentromedico).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var rescentromedico = await GetById(id);
            if (rescentromedico == null) return false;
            db.ResCentroMedico.Remove(rescentromedico);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}