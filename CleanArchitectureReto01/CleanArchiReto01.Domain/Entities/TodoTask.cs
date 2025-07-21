namespace CleanArchiReto01.Domain.Entities
{
	public class TodoTask
    {
        public Guid Id { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public bool IsCompleted { get; private set; }

        public TodoTask(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");

            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            IsCompleted = false;
        }

        public void Complete()
        {
            IsCompleted = true;
        }
    }

}