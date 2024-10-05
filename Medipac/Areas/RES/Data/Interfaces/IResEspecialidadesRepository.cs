using Medipac.Models;

namespace Medipac.Areas.RES.Data.Interfaces
{
    public interface IResEspecialidadesRepository
    {
        Task<List<ResEspecialidades>> GetAll();
        Task<ResEspecialidades> GetById(int id);
        Task<ResEspecialidades> Add(ResEspecialidades resespecialidades);
        void Update(ResEspecialidades resespecialidades);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}