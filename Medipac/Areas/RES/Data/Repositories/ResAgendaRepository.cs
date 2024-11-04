using Medipac.Areas.RES.Data.Interfaces;
using Medipac.Context;
using Medipac.Models;
using Microsoft.EntityFrameworkCore;


namespace Medipac.Data.Repositories
{
    public class ResAgendaRepository : IResAgendaRepository
    {
        private readonly DbMedipac db;

        public ResAgendaRepository(DbMedipac db)
        {
            this.db = db;
        }

        public async Task<List<ResAgenda>> GetAll()
        {
            return await db.ResAgenda.ToListAsync();
        }

        public async Task<ResAgenda> GetById(int id) => await db.ResAgenda
            .FirstOrDefaultAsync(a => a.IdAgenda == id) ?? new ResAgenda();

        public async Task<ResAgenda> Add(ResAgenda resagenda)
        {
            _ = await db.ResAgenda.AddAsync(resagenda);
            return resagenda;
        }

        public void Update(ResAgenda resagenda)
        {
            _ = db.ResAgenda.Update(resagenda);
        }

        public async Task<bool> DeleteById(int id)
        {
            var entity = await db.ResAgenda.FindAsync(id);
            if (entity != null)
            {
                _ = db.ResAgenda.Remove(entity);
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

        public async Task<bool> ExisteConflictoHorario(int idMedico, DateOnly fecha, int horaInicio, int horaFin, int? idExcluir = null)
        {
            return await db.ResAgenda.AnyAsync(a =>
                a.IdMedico == idMedico &&
                a.Fecha == fecha &&
                ((horaInicio >= a.HoraInicio && horaInicio < a.HoraFin) ||
                 (horaFin > a.HoraInicio && horaFin <= a.HoraFin) ||
                 (horaInicio <= a.HoraInicio && horaFin >= a.HoraFin)) &&
                (!idExcluir.HasValue || a.IdAgenda != idExcluir) // Excluir el ID actual si se proporciona
            );
        }
    }
}