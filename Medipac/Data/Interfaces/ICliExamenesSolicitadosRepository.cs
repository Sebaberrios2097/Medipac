using Medipac.Models;

namespace Medipac.Data.Interfaces
{
    public interface ICliExamenesSolicitadosRepository
    {
        Task<List<CliExamenesSolicitados>> GetAll();
        Task<CliExamenesSolicitados> GetById(int id);
        Task<CliExamenesSolicitados> Add(CliExamenesSolicitados cliexamenessolicitados);
        void Update(CliExamenesSolicitados cliexamenessolicitados);
        Task<bool> DeleteById(int id);
        Task<int> Save();
    }
}