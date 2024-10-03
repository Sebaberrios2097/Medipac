using Medipac.Models;

namespace Medipac.Areas.CLI.Data.Interfaces
{
    public interface ICliSolicitudExamenRepository
    {
        Task<List<CliSolicitudExamen>> GetAll();
        Task<CliSolicitudExamen> GetById(int id);
        Task<CliSolicitudExamen> Add(CliSolicitudExamen clisolicitudexamen);
        void Update(CliSolicitudExamen clisolicitudexamen);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}