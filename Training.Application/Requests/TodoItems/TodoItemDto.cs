namespace Training.Application.Requests.TodoItems;

public class TodoItemDto
{
    public Guid Id { get; set; }

    public string Text { get; set; } = default!;

    public bool IsCompleted { get; set; }
}
