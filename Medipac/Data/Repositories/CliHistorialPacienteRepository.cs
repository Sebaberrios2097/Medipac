using Medipac.Models;
using Medipac.Context;
using Medipac.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class CliHistorialPacienteRepository : ICliHistorialPacienteRepository
    {
        private readonly DbMedipac db;

        public CliHistorialPacienteRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<CliHistorialPaciente>> GetAll()
        {
            return await db.CliHistorialPaciente.ToListAsync();
        }

        public async Task<CliHistorialPaciente> GetById(int id) => await db.CliHistorialPaciente
            .FirstOrDefaultAsync(a => a.Id == id) ?? new CliHistorialPaciente();

        public async Task<CliHistorialPaciente> Add(CliHistorialPaciente clihistorialpaciente)
        {
            db.CliHistorialPaciente.Add(clihistorialpaciente);
            await Save();
            return clihistorialpaciente;
        }

        public void Update(CliHistorialPaciente clihistorialpaciente)
        {
            db.Entry(clihistorialpaciente).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var clihistorialpaciente = await GetById(id);
            if (clihistorialpaciente == null) return false;
            db.CliHistorialPaciente.Remove(clihistorialpaciente);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}