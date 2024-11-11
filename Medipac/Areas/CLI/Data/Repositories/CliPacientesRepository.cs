using Medipac.Areas.CLI.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;


namespace Medipac.Data.Repositories
{
    public class CliPacientesRepository : ICliPacientesRepository
    {
        private readonly DbMedipac db;

        public CliPacientesRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<CliPacientes>> GetAll()
        {
            return await db.CliPacientes.ToListAsync();
        }

        public async Task<CliPacientes> GetById(int id) => await db.CliPacientes
            .FirstOrDefaultAsync(a => a.IdPaciente == id) ?? new CliPacientes();

        public async Task<CliPacientes> GetByUserId(string id)
        {
            var idToInt = int.Parse(id);
            return await db.CliPacientes.Where(c => c.IdUsuario == idToInt).FirstOrDefaultAsync() ?? new CliPacientes();
        }

        public async Task<CliPacientes> Add(CliPacientes clipacientes)
        {
            _ = await db.CliPacientes.AddAsync(clipacientes);
            return clipacientes;
        }

        public void Update(CliPacientes clipacientes)
        {
            _ = db.CliPacientes.Update(clipacientes);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.CliPacientes.FindAsync(id);
            if (entity != null)
            {
                _ = db.CliPacientes.Remove(entity);
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