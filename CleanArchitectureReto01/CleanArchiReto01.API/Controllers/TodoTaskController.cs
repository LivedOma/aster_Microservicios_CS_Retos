using Microsoft.AspNetCore.Mvc;
using CleanArchiReto01.Application.UseCases;
using CleanArchiReto01.Domain.Interfaces;
using CleanArchiReto01.Domain.Entities;


namespace CleanArchiReto01.API.Controllers
{   
    /// <summary>
    /// Controller for managing users in the Clean Architecture application.
    /// Provides endpoints to create and retrieve users.
    /// </summary>
    [ApiController]
    [Route("api/todoTasks")]
    public class TodoTaskController : ControllerBase
    {
        private readonly CreateTodoTaskUseCase _createTodoTaskUseCase;
        private readonly ITodoTaskRepository _todoTaskRepository;

        public TodoTaskController(CreateTodoTaskUseCase createTodoTaskUseCase, ITodoTaskRepository todoTaskRepository)
        {
            _createTodoTaskUseCase = createTodoTaskUseCase;
            _todoTaskRepository = todoTaskRepository;
        }

        /// <summary>
        /// Creates a new todo task in the system.
        /// Accepts a TodoTask object and returns the created todo task with a 201 Created status.
        /// </summary>
        /// <param name="todoTask"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateTodoTask(TodoTask todoTask)
        {
            try
            {
                var title = todoTask.Title.ToString();
                var description = todoTask.Description.ToString();

                var createdTodoTask = await _createTodoTaskUseCase.Execute(title, description);
                return CreatedAtAction(nameof(GetTodoTasks), new { id = createdTodoTask.Id }, createdTodoTask);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves all users from the system.
        /// Returns a list of users with a 200 OK status.
        /// If an error occurs, returns a 400 Bad Request with the error message.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTodoTasks()
        {
            try
            {
                var todoTasks = await _todoTaskRepository.GetAll();
                return Ok(todoTasks);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}