
using System;
using SmartAPI.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAPI.Data.Repositories
{
    public interface IWorkerRepository
    {
        Task<IEnumerable<Worker>> GetAllWorkers();
        Task<Worker> GetDetails(int id);
        Task<bool> InsertWorker(Worker worker);
        Task<bool> UpdateWorker(Worker worker);
        Task<bool> DeleteWorker(Worker worker);

    }
}
