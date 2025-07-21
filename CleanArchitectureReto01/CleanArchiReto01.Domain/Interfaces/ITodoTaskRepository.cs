using CleanArchiReto01.Domain.Entities;

namespace CleanArchiReto01.Domain.Interfaces
{
    public interface ITodoTaskRepository
    {
        Task Save(TodoTask todoTask);
        Task<IEnumerable<TodoTask>> GetAll();
        Task<TodoTask> GetById(Guid id);
        Task Update(TodoTask todoTask);
    }
}