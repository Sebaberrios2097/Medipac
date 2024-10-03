using Medipac.Models;
using Medipac.Context;
using Microsoft.EntityFrameworkCore;
using Medipac.Areas.CLI.Data.Interfaces;

namespace Medipac.Areas.CLI.Data.Repositories
{
    public class CliExamenesSolicitadosRepository(DbMedipac db) : ICliExamenesSolicitadosRepository
    {
        public readonly DbMedipac db = db;

        public async Task<List<CliExamenesSolicitados>> GetAll()
        {
            return await db.CliExamenesSolicitados.ToListAsync();
        }

        public async Task<CliExamenesSolicitados> GetById(int id) => await db.CliExamenesSolicitados
            .FirstOrDefaultAsync(a => a.IdExamenesSolicitados == id) ?? new CliExamenesSolicitados();

        public async Task<CliExamenesSolicitados> Add(CliExamenesSolicitados cliexamenessolicitados)
        {
            db.CliExamenesSolicitados.Add(cliexamenessolicitados);
            await Save();
            return cliexamenessolicitados;
        }

        public void Update(CliExamenesSolicitados cliexamenessolicitados)
        {
            db.Entry(cliexamenessolicitados).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var cliexamenessolicitados = await GetById(id);
            if (cliexamenessolicitados == null) return false;
            db.CliExamenesSolicitados.Remove(cliexamenessolicitados);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}