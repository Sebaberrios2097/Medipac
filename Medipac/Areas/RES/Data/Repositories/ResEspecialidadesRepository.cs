using Medipac.Areas.RES.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class ResEspecialidadesRepository : IResEspecialidadesRepository
    {
        private readonly DbMedipac db;

        public ResEspecialidadesRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<ResEspecialidades>> GetAll()
        {
            return await db.ResEspecialidades.ToListAsync();
        }

        public async Task<ResEspecialidades> GetById(int id) => await db.ResEspecialidades
            .FirstOrDefaultAsync(a => a.IdEspecialidad == id) ?? new ResEspecialidades();

        public async Task<ResEspecialidades> Add(ResEspecialidades resespecialidades)
        {
            _ = await db.ResEspecialidades.AddAsync(resespecialidades);
            return resespecialidades;
        }

        public void Update(ResEspecialidades resespecialidades)
        {
            _ = db.ResEspecialidades.Update(resespecialidades);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.ResEspecialidades.FindAsync(id);
            if (entity != null)
            {
                _ = db.ResEspecialidades.Remove(entity);
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