namespace Training.Application.Requests.TodoItems.Queries.GetTodoItems;

public class GetTodoItemsQueryViewModel
{
    public IEnumerable<TodoItemDto> Items { get; set; } = default!;
}
