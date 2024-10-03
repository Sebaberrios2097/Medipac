using Medipac.Models;
using Medipac.Context;
using Microsoft.EntityFrameworkCore;
using Medipac.Areas.CLI.Data.Interfaces;

namespace Medipac.Areas.CLI.Data.Repositories
{
    public class CliPacientesRepository(DbMedipac db) : ICliPacientesRepository
    {
        public readonly DbMedipac db = db;

        public async Task<List<CliPacientes>> GetAll()
        {
            return await db.CliPacientes.ToListAsync();
        }

        public async Task<CliPacientes> GetById(int id) => await db.CliPacientes
            .FirstOrDefaultAsync(a => a.IdPaciente == id) ?? new CliPacientes();

        public async Task<CliPacientes> Add(CliPacientes clipacientes)
        {
            db.CliPacientes.Add(clipacientes);
            await Save();
            return clipacientes;
        }

        public void Update(CliPacientes clipacientes)
        {
            db.Entry(clipacientes).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var clipacientes = await GetById(id);
            if (clipacientes == null) return false;
            db.CliPacientes.Remove(clipacientes);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}