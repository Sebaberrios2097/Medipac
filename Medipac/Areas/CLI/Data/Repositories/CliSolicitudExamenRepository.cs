using Medipac.Models;
using Medipac.Context;
using Microsoft.EntityFrameworkCore;
using Medipac.Areas.CLI.Data.Interfaces;

namespace Medipac.Areas.CLI.Data.Repositories
{
    public class CliSolicitudExamenRepository(DbMedipac db) : ICliSolicitudExamenRepository
    {
        public readonly DbMedipac db = db;

        public async Task<List<CliSolicitudExamen>> GetAll()
        {
            return await db.CliSolicitudExamen.ToListAsync();
        }

        public async Task<CliSolicitudExamen> GetById(int id) => await db.CliSolicitudExamen
            .FirstOrDefaultAsync(a => a.IdSolicitudExamen == id) ?? new CliSolicitudExamen();

        public async Task<CliSolicitudExamen> Add(CliSolicitudExamen clisolicitudexamen)
        {
            db.CliSolicitudExamen.Add(clisolicitudexamen);
            await Save();
            return clisolicitudexamen;
        }

        public void Update(CliSolicitudExamen clisolicitudexamen)
        {
            db.Entry(clisolicitudexamen).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var clisolicitudexamen = await GetById(id);
            if (clisolicitudexamen == null) return false;
            db.CliSolicitudExamen.Remove(clisolicitudexamen);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}