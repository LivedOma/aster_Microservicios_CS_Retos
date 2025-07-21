using Microsoft.AspNetCore.Mvc;
using CleanArchiReto01.Application.UseCases;
using CleanArchiReto01.Domain.Interfaces;
using CleanArchiReto01.Domain.Entities;


namespace CleanArchiReto01.API.Controllers
{
    /// <summary>
    /// Controlador para gestionar las tareas pendientes en la aplicación Clean Architecture.
    /// Proporciona endpoints para crear y recuperar tareas.
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
        /// Crea una nueva tarea en el sistema.
        /// Accepta un objeto TodoTask y devuelve la tarea creada con un estado 201 Created.
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
        /// Obtiene todas las tareas del sistema.
        /// Retorna una lista de tareas con un estado 200 OK.
        /// Si ocurre un error, devuelve un estado 400 Bad Request con el mensaje de error.
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
        
        /// <summary>
        /// Completa una tarea por su ID.
        /// Accepta un Guid ID y marca la tarea correspondiente como completada.
        /// Retorna un estado 204 No Content si es exitoso.
        /// Si la tarea no se encuentra, retorna un estado 404 Not Found con un mensaje de error.
        /// Si ocurre un error, retorna un estado 400 Bad Request con el mensaje de error.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("complete/{id}")]
        public async Task<IActionResult> CompleteTodoTask(Guid id)
        {
            try
            {
                var completeTodoTaskUseCase = new CompleteTodoTaskUseCase(_todoTaskRepository);
                await completeTodoTaskUseCase.Execute(id);
                return NoContent(); // 204 No Content
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}