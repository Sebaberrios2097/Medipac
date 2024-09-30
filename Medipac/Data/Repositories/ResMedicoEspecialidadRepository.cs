using Medipac.Models;
using Medipac.Context;
using Medipac.Data.Interfaces;
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
            .FirstOrDefaultAsync(a => a.Id == id) ?? new ResMedicoEspecialidad();

        public async Task<ResMedicoEspecialidad> Add(ResMedicoEspecialidad resmedicoespecialidad)
        {
            db.ResMedicoEspecialidad.Add(resmedicoespecialidad);
            await Save();
            return resmedicoespecialidad;
        }

        public void Update(ResMedicoEspecialidad resmedicoespecialidad)
        {
            db.Entry(resmedicoespecialidad).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var resmedicoespecialidad = await GetById(id);
            if (resmedicoespecialidad == null) return false;
            db.ResMedicoEspecialidad.Remove(resmedicoespecialidad);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}