using Medipac.Areas.CLI.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
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
            .FirstOrDefaultAsync(a => a.IdMedico == id) ?? new CliMedico();

        public async Task<CliMedico> Add(CliMedico climedico)
        {
            _ = await db.CliMedico.AddAsync(climedico);
            return climedico;
        }

        public void Update(CliMedico climedico)
        {
            _ = db.CliMedico.Update(climedico);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.CliMedico.FindAsync(id);
            if (entity != null)
            {
                _ = db.CliMedico.Remove(entity);
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