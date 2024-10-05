using Medipac.Areas.CLI.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;


namespace Medipac.Data.Repositories
{
    public class CliRecetaPacienteRepository : ICliRecetaPacienteRepository
    {
        private readonly DbMedipac db;

        public CliRecetaPacienteRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<CliRecetaPaciente>> GetAll()
        {
            return await db.CliRecetaPaciente.ToListAsync();
        }

        public async Task<CliRecetaPaciente> GetById(int id) => await db.CliRecetaPaciente
            .FirstOrDefaultAsync(a => a.IdRecetaPaciente == id) ?? new CliRecetaPaciente();

        public async Task<CliRecetaPaciente> Add(CliRecetaPaciente clirecetapaciente)
        {
            _ = await db.CliRecetaPaciente.AddAsync(clirecetapaciente);
            return clirecetapaciente;
        }

        public void Update(CliRecetaPaciente clirecetapaciente)
        {
            _ = db.CliRecetaPaciente.Update(clirecetapaciente);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.CliRecetaPaciente.FindAsync(id);
            if (entity != null)
            {
                _ = db.CliRecetaPaciente.Remove(entity);
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