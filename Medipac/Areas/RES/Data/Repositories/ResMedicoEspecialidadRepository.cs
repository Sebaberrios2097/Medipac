using Medipac.Areas.RES.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class ResMedicoEspecialidadRepository : IResMedicoEspecialidadRepository
    {
        private readonly DbMedipac db;

        public ResMedicoEspecialidadRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<ResMedicoEspecialidad>> GetAll()
        {
            return await db.ResMedicoEspecialidad.ToListAsync();
        }

        public async Task<ResMedicoEspecialidad> GetById(int id) => await db.ResMedicoEspecialidad
            .FirstOrDefaultAsync(a => a.IdMedicoEspecialidad == id) ?? new ResMedicoEspecialidad();

        public async Task<ResMedicoEspecialidad> Add(ResMedicoEspecialidad resmedicoespecialidad)
        {
            _ = await db.ResMedicoEspecialidad.AddAsync(resmedicoespecialidad);
            return resmedicoespecialidad;
        }

        public void Update(ResMedicoEspecialidad resmedicoespecialidad)
        {
            _ = db.ResMedicoEspecialidad.Update(resmedicoespecialidad);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.ResMedicoEspecialidad.FindAsync(id);
            if (entity != null)
            {
                _ = db.ResMedicoEspecialidad.Remove(entity);
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