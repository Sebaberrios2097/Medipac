using Medipac.Areas.RES.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class ResReservaRepository : IResReservaRepository
    {
        private readonly DbMedipac db;

        public ResReservaRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<ResReserva>> GetAll()
        {
            return await db.ResReserva.ToListAsync();
        }

        public async Task<ResReserva> GetById(int id) => await db.ResReserva
            .FirstOrDefaultAsync(a => a.IdReserva == id) ?? new ResReserva();

        public async Task<ResReserva> Add(ResReserva resreserva)
        {
            _ = await db.ResReserva.AddAsync(resreserva);
            return resreserva;
        }

        public void Update(ResReserva resreserva)
        {
            _ = db.ResReserva.Update(resreserva);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.ResReserva.FindAsync(id);
            if (entity != null)
            {
                _ = db.ResReserva.Remove(entity);
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

        public async Task<IEnumerable<ResEspecialidades>> GetEspecialidades()
        {
            return await db.ResEspecialidades.Where(e => e.Estado).ToListAsync();
        }
        public async Task<IEnumerable<CliMedico>> GetMedicosByEspecialidad(int idEspecialidad)
        {
            return await db.ResMedicoEspecialidad
                .Where(me => me.IdEspecialidad == idEspecialidad)
                .Select(me => me.IdMedicoNavigation)
                .ToListAsync();
        }
    }
}