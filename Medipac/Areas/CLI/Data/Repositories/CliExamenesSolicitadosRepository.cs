using Medipac.Areas.CLI.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class CliExamenesSolicitadosRepository : ICliExamenesSolicitadosRepository
    {
        private readonly DbMedipac db;

        public CliExamenesSolicitadosRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<CliExamenesSolicitados>> GetAll()
        {
            return await db.CliExamenesSolicitados.ToListAsync();
        }

        public async Task<CliExamenesSolicitados> GetById(int id) => await db.CliExamenesSolicitados
            .FirstOrDefaultAsync(a => a.IdExamenesSolicitados == id) ?? new CliExamenesSolicitados();

        public async Task<CliExamenesSolicitados> Add(CliExamenesSolicitados cliexamenessolicitados)
        {
            _ = await db.CliExamenesSolicitados.AddAsync(cliexamenessolicitados);
            return cliexamenessolicitados;
        }

        public void Update(CliExamenesSolicitados cliexamenessolicitados)
        {
            _ = db.CliExamenesSolicitados.Update(cliexamenessolicitados);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.CliExamenesSolicitados.FindAsync(id);
            if (entity != null)
            {
                _ = db.CliExamenesSolicitados.Remove(entity);
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