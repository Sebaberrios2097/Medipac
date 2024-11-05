using Medipac.Areas.RES.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;

namespace Medipac.Data.Repositories
{
    public class ResHorarioMedicoRepository : IResHorarioMedicoRepository
    {
        private readonly DbMedipac db;

        public ResHorarioMedicoRepository(DbMedipac db)
        {
            this.db = db;
        }

        // Obtener todos los horarios de los médicos
        public async Task<List<ResHorarioMedico>> GetAll()
        {
            return await db.ResHorarioMedico.ToListAsync();
        }

        // Obtener todos los horarios de un médico específico
        public async Task<List<ResHorarioMedico>> GetByMedicoId(int id)
        {
            return await db.ResHorarioMedico
                           .Where(c => c.IdMedico == id)
                           .ToListAsync();
        }

        // Obtener un horario específico por su ID
        public async Task<ResHorarioMedico> GetById(int id)
        {
            return await db.ResHorarioMedico
                           .FirstOrDefaultAsync(a => a.IdHorario == id)
                           ?? new ResHorarioMedico();
        }

        // Agregar un nuevo horario para un médico
        public async Task<ResHorarioMedico> Add(ResHorarioMedico resHorarioMedico)
        {
            await db.ResHorarioMedico.AddAsync(resHorarioMedico);
            return resHorarioMedico;
        }

        // Actualizar un horario existente
        public void Update(ResHorarioMedico resHorarioMedico)
        {
            db.ResHorarioMedico.Update(resHorarioMedico);
        }

        // Eliminar un horario específico por su ID
        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.ResHorarioMedico.FindAsync(id);
            if (entity != null)
            {
                db.ResHorarioMedico.Remove(entity);
                return true;
            }
            return false;
        }

        // Guardar los cambios realizados en la base de datos
        public async Task<int> Save()
        {
            return await db.SaveChangesAsync();
        }
    }
}
