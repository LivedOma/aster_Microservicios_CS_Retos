using CleanArchiReto01.Domain.Interfaces;

namespace CleanArchiReto01.Application.UseCases
{
    public class CompleteTodoTaskUseCase
    {
        private readonly ITodoTaskRepository _todoTaskRepository;

        public CompleteTodoTaskUseCase(ITodoTaskRepository todoTaskRepository)
        {
            _todoTaskRepository = todoTaskRepository;
        }

        public async Task Execute(Guid id)
        {
            var todoTask = await _todoTaskRepository.GetById(id);
            todoTask.Complete();
            await _todoTaskRepository.Update(todoTask);
        }
    }
}