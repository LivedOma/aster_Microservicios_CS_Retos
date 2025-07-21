using CleanArchiReto01.Domain.Entities;
using CleanArchiReto01.Domain.Interfaces;

namespace CleanArchiReto01.Infrastructure.Persistence
{
    public class TodoTaskRepository : ITodoTaskRepository
    {
        private readonly List<TodoTask> _todoTasks = new();

        public Task Save(TodoTask todoTask)
        {
            _todoTasks.Add(todoTask);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<TodoTask>> GetAll()
        {
            return Task.FromResult<IEnumerable<TodoTask>>(_todoTasks);
        }
    }
}