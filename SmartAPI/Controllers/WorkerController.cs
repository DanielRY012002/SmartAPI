using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAPI.Data.Repositories;
using SmartAPI.Model;

namespace SmartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerRepository _workerRepository;
        public WorkerController(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }
        [HttpGet]
        public async Task<IActionResult> getAllWorkers()
        {
            return Ok(await _workerRepository.GetAllWorkers());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult>getWorkerDetil(int id)
        {
            return Ok(await _workerRepository.GetDetails(id));
        }
        [HttpPost]
        public async Task<IActionResult> createWorker([FromBody] Worker worker)
        {
            if (worker == null)
                return BadRequest();
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var created = await _workerRepository.InsertWorker(worker);
            return Created("created", created);
        }
        [HttpPut]
        public async Task<IActionResult> updateWorker(Worker worker)
        {
            if (worker == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _workerRepository.UpdateWorker(worker);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> deleteWorker(int id)
        {
            await _workerRepository.DeleteWorker(new Worker { Id = id });
            return NoContent();
        }
    } 
}