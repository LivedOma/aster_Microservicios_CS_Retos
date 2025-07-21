using CleanArchiReto01.Domain.Entities;
using CleanArchiReto01.Domain.Interfaces;

namespace CleanArchiReto01.Application.UseCases
{
    public class CreateTodoTaskUseCase
    {
        private readonly ITodoTaskRepository _todoTaskRepository;

        public CreateTodoTaskUseCase(ITodoTaskRepository todoTaskRepository)
        {
            _todoTaskRepository = todoTaskRepository;
        }

        public async Task<TodoTask> Execute(string title, string description)
        {
            var todoTask = new TodoTask(title, description);
            await _todoTaskRepository.Save(todoTask);
            return todoTask;
        }
    }
}