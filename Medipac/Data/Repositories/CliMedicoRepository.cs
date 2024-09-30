using Medipac.Models;
using Medipac.Context;
using Medipac.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class CliMedicoRepository : ICliMedicoRepository
    {
        private readonly DbMedipac db;

        public CliMedicoRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<CliMedico>> GetAll()
        {
            return await db.CliMedico.ToListAsync();
        }

        public async Task<CliMedico> GetById(int id) => await db.CliMedico
            .FirstOrDefaultAsync(a => a.Id == id) ?? new CliMedico();

        public async Task<CliMedico> Add(CliMedico climedico)
        {
            db.CliMedico.Add(climedico);
            await Save();
            return climedico;
        }

        public void Update(CliMedico climedico)
        {
            db.Entry(climedico).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var climedico = await GetById(id);
            if (climedico == null) return false;
            db.CliMedico.Remove(climedico);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}