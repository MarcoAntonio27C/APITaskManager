using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Models;
using TaskManager.API.Services;
using Task = TaskManager.API.Models.Task;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Ruta: /api/tareas
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }


        [HttpGet]
        public IActionResult GetTasks()
        {
            var tasks = _taskService.GetTasks();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public ActionResult<Task> Get(Guid id)
        {
            var tarea = _taskService.GetById(id);
            if (tarea == null)
            {
                return NotFound();
            }
            return Ok(tarea);
        }

        [HttpPost]
        public ActionResult<Task> Create([FromBody] Task newTask)
        {
            var createdTask = _taskService.Create(newTask);
            return CreatedAtAction(nameof(Get), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}/status")]
        public ActionResult UpdateStatus(Guid id, [FromQuery] bool completed)
        {
            var updatedTask = _taskService.UpdateStatus(id, completed);
            if (updatedTask == null)
            {
                return NotFound($"Tarea ID {id} no encontrada o el estado no cambió.");
            }
            return NoContent(); // 204 indica éxito sin contenido que retornar

        }

    }
}
