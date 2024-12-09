using Medipac.Areas.CLI.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
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

        public async Task<CliHistorialPaciente> GetHistorialByIdMedicoYPaciente(int idMedico, int idPaciente)
        {
            return await db.CliHistorialPaciente.FirstOrDefaultAsync(h => h.IdMedico == idMedico && h.IdPaciente == idPaciente);
        }

        public async Task<CliHistorialPaciente> GetById(int id) => await db.CliHistorialPaciente
            .FirstOrDefaultAsync(a => a.IdHistorialPaciente == id) ?? new CliHistorialPaciente();

        public async Task<CliHistorialPaciente> Add(CliHistorialPaciente clihistorialpaciente)
        {
            _ = await db.CliHistorialPaciente.AddAsync(clihistorialpaciente);
            return clihistorialpaciente;
        }

        public void Update(CliHistorialPaciente clihistorialpaciente)
        {
            _ = db.CliHistorialPaciente.Update(clihistorialpaciente);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.CliHistorialPaciente.FindAsync(id);
            if (entity != null)
            {
                _ = db.CliHistorialPaciente.Remove(entity);
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