using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;
using Endpoint.API.Interfaces;
using Endpoint.API.Models;

namespace Endpoint.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/task")]
    [EnableRateLimiting("fixedwindow")]
    public class TaskController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Returns all tasks.")]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetAllTasks()
        {
            var tasks = await _unitOfWork.TasksRepository.GetAllAsync();

            if (tasks is null)
                return NotFound("No tasks.");

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Returns a task by ID.")]
        public async Task<ActionResult<Tasks>> GetTaskById(int id)
        {
            var task = await _unitOfWork.TasksRepository.GetByIdAsync(id);

            if (task is null)
                return NotFound("Task not found.");

            return Ok(task);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new task.")]
        public async Task<ActionResult<Tasks>> AddTask(Tasks task)
        {
            var newTask = await _unitOfWork.TasksRepository.AddAsync(task);

            if (newTask is null)
                return BadRequest("Error adding task.");

            return Ok(newTask);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Updates an existing task.")]
        public async Task<ActionResult<Tasks>> UpdateTask(Tasks task)
        {
            var updatedTask = await _unitOfWork.TasksRepository.UpdateAsync(task);

            if (updatedTask is null)
                return BadRequest("Error updating task.");

            return Ok(updatedTask);
        }

        [HttpDelete]
        [SwaggerOperation(Summary = "Removes an existing task.")]
        public async Task<ActionResult<Tasks>> RemoveTask(int id)
        {
            var task = await _unitOfWork.TasksRepository.GetByIdAsync(id);

            if (task is null)
                return BadRequest("Error removing task.");

            await _unitOfWork.TasksRepository.RemoveAsync(task);

            return Ok(task);
        }

        [HttpGet("filter/due-date")]
        [SwaggerOperation(Summary = "Filter tasks by due date.", 
            Description = "Filters tasks based on due date given in yyyy-MM-dd format.")]
        public async Task<ActionResult<IEnumerable<Tasks>>> FilterTasksByDate(DateTime date)
        {
            var tasks = await _unitOfWork.TasksRepository.FilterByDateAsync(date);

            if (tasks is null)
                return NotFound("No tasks found.");

            return Ok(tasks);
        }

        [HttpGet("filter/priority")]
        [SwaggerOperation(Summary = "Filter tasks by priority.",
            Description = "Filters tasks based on the given priority: \"Low\", \"Medium\" or \"High\".")]
        public async Task<ActionResult<IEnumerable<Tasks>>> FilterTasksByPriority(string priority)
        {
            var tasks = await _unitOfWork.TasksRepository.FilterByPriorityAsync(priority);

            if (tasks is null || priority is null || priority != "Low" && priority != "Medium" && priority != "High")
                return NotFound("No tasks found.");

            return Ok(tasks);
        }

        [HttpGet("filter/status")]
        [SwaggerOperation(Summary = "Filter tasks by status.",
            Description = "Filters tasks based on status: Completed (true) or Not Completed (false).")]
        public async Task<ActionResult<IEnumerable<Tasks>>> FilterTasksByStatus(bool status)
        {
            var tasks = await _unitOfWork.TasksRepository.FilterByStatusAsync(status);

            if (tasks is null)
                return NotFound("No tasks found.");

            return Ok(tasks);
        }
    }
}
