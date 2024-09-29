using Medipac.Models;
using Medipac.Context;
using Medipac.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class ResEspecialidadesRepository(DbMedipac db) : IResEspecialidadesRepository
    {
        public readonly DbMedipac db = db;

        public async Task<List<ResEspecialidades>> GetAll()
        {
            return await db.ResEspecialidades.ToListAsync();
        }

        public async Task<ResEspecialidades> GetById(int id) => await db.ResEspecialidades
            .FirstOrDefaultAsync(a => a.IdEspecialidad == id) ?? new ResEspecialidades();

        public async Task<ResEspecialidades> Add(ResEspecialidades resespecialidades)
        {
            db.ResEspecialidades.Add(resespecialidades);
            await Save();
            return resespecialidades;
        }

        public void Update(ResEspecialidades resespecialidades)
        {
            db.Entry(resespecialidades).State = EntityState.Modified;
        }

        public async Task<bool> DeleteById(int id)
        {
            var resespecialidades = await GetById(id);
            if (resespecialidades == null) return false;
            db.ResEspecialidades.Remove(resespecialidades);
            return true;
        }

        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}