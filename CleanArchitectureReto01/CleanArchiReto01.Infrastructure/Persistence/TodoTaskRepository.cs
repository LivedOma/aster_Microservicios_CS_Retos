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

        public Task<TodoTask> GetById(Guid id)
        {
            TodoTask? result = _todoTasks.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                throw new InvalidOperationException($"TodoTask with ID {id} not found.");
            }
            return Task.FromResult(result);
        }

        public Task Update(TodoTask todoTask)
        {
            var existingTask = _todoTasks.FirstOrDefault(x => x.Id == todoTask.Id);
            if (existingTask == null)
            {
                throw new InvalidOperationException($"TodoTask with ID {todoTask.Id} not found.");
            }

            existingTask = todoTask; // Update the existing task with the new values
            return Task.CompletedTask;
        }
    }
}