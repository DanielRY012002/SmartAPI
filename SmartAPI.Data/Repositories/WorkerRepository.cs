using Dapper;
using MySql.Data.MySqlClient;
using SmartAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SmartAPI.Data.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly MySQLConfiguration _connectionString;
        public WorkerRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }
        async Task<bool> IWorkerRepository.DeleteWorker(Worker worker)
        {
            var db = dbConnection();
            var sql = @"DELETE FROM WORKER WHERE ID=@Id";
            var result = await db.ExecuteAsync(sql, new { Id = worker.Id });
            return result > 0;
        }
        Task<IEnumerable<Worker>> IWorkerRepository.GetAllWorkers()
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM WORKER";
            return db.QueryAsync<Worker>(sql, new { });
        }
        Task<Worker> IWorkerRepository.GetDetails(int id)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM WORKER WHERE ID=@id";
            return db.QueryFirstOrDefaultAsync<Worker>(sql, new { Id = id });
        }
        async Task<bool> IWorkerRepository.InsertWorker(Worker worker)
        {
            var db = dbConnection();
            var sql = @"INSERT INTO WORKER(DNI,NAME,LASTNAME,BIRTHDAY,GENDER,ADDRESS,SALARY)VALUES(@Dni,@Name,@Lastname,@Birthday,@Gender,@Address,@Salary)";
            var result = await db.ExecuteAsync(sql, new { worker.Dni, worker.Name, worker.Lastname, worker.Birthday, worker.Gender, worker.Address, worker.Salary });
            return result > 0;
        }
        async Task<bool> IWorkerRepository.UpdateWorker(Worker worker)
        {
            var db = dbConnection();
            var sql = @"UPDATE WORKER SET DNI=@Dni,NAME=@Name,LASTNAME=@Lastname,BIRTHDAY=@Birthday,GENDER=@Gender,ADDRESS=@Address,SALARY=@Salary WHERE ID=@Id";
            var result = await db.ExecuteAsync(sql, new { worker.Dni, worker.Name, worker.Lastname, worker.Birthday, worker.Gender, worker.Address, worker.Salary, worker.Id });
            return result > 0;
        }
    }
}