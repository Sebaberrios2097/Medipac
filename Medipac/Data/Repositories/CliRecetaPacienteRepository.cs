using Medipac.Models;
using Medipac.Context;
using Medipac.Data.Interfaces;
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
            .FirstOrDefaultAsync(a => a.Id == id) ?? new CliRecetaPaciente();

        public async Task<CliRecetaPaciente> Add(CliRecetaPaciente clirecetapaciente)
        {
            db.CliRecetaPaciente.Add(clirecetapaciente);
            await Save();
            return clirecetapaciente;
        }

        public void Update(CliRecetaPaciente clirecetapaciente)
        {
            db.Entry(clirecetapaciente).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var clirecetapaciente = await GetById(id);
            if (clirecetapaciente == null) return false;
            db.CliRecetaPaciente.Remove(clirecetapaciente);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}