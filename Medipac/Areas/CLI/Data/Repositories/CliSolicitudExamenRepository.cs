using Medipac.Areas.CLI.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;


namespace Medipac.Data.Repositories
{
    public class CliSolicitudExamenRepository : ICliSolicitudExamenRepository
    {
        private readonly DbMedipac db;

        public CliSolicitudExamenRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<CliSolicitudExamen>> GetAll()
        {
            return await db.CliSolicitudExamen.ToListAsync();
        }

        public async Task<CliSolicitudExamen> GetById(int id) => await db.CliSolicitudExamen
            .FirstOrDefaultAsync(a => a.IdSolicitudExamen == id) ?? new CliSolicitudExamen();

        public async Task<CliSolicitudExamen> Add(CliSolicitudExamen clisolicitudexamen)
        {
            _ = await db.CliSolicitudExamen.AddAsync(clisolicitudexamen);
            return clisolicitudexamen;
        }

        public void Update(CliSolicitudExamen clisolicitudexamen)
        {
            _ = db.CliSolicitudExamen.Update(clisolicitudexamen);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.CliSolicitudExamen.FindAsync(id);
            if (entity != null)
            {
                _ = db.CliSolicitudExamen.Remove(entity);
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