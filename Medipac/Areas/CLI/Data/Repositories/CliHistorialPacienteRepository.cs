using Medipac.Models;
using Medipac.Context;
using Microsoft.EntityFrameworkCore;
using Medipac.Areas.CLI.Data.Interfaces;

namespace Medipac.Areas.CLI.Data.Repositories
{
    public class CliHistorialPacienteRepository(DbMedipac db) : ICliHistorialPacienteRepository
    {
        public readonly DbMedipac db = db;

        public async Task<List<CliHistorialPaciente>> GetAll()
        {
            return await db.CliHistorialPaciente.ToListAsync();
        }

        public async Task<CliHistorialPaciente> GetById(int id) => await db.CliHistorialPaciente
            .FirstOrDefaultAsync(a => a.IdHistorialPaciente == id) ?? new CliHistorialPaciente();

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