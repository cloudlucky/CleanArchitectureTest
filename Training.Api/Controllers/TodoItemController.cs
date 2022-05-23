using MediatR;
using Microsoft.AspNetCore.Mvc;
using Training.Application.Requests.TodoItems;
using Training.Application.Requests.TodoItems.Commands.CreateTodoItem;
using Training.Application.Requests.TodoItems.Commands.UpdateTodoItem;
using Training.Application.Requests.TodoItems.Queries.GetTodoItemById;
using Training.Application.Requests.TodoItems.Queries.GetTodoItems;

namespace Training.Api.Controllers;

[ApiController]
[Route("api/TodoItems")]
public class TodoItemController : Controller
{
    private readonly IMediator mediator;

    public TodoItemController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<GetTodoItemsQueryViewModel> Get(CancellationToken cancellationToken)
    {
        var result = await this.mediator.Send(new GetTodoItemsQuery(), cancellationToken);

        return result;
    }

    [HttpGet("{id:guid}")]
    public async Task<GetTodoItemByIdQueryViewModel> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await this.mediator.Send(new GetTodoItemByIdQuery
        {
            Id = id
        }, cancellationToken);

        return result;
    }

    [HttpPost]
    public async Task Post(TodoItemDto dto, CancellationToken cancellationToken)
    {
        await this.mediator.Send(new CreateTodoItemCommand
        {
            TodoItemDto = dto
        }, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task Put(Guid id, TodoItemDto dto, CancellationToken cancellationToken)
    {
        await this.mediator.Send(new UpdateTodoItemCommand
        {
            Id = id,
            Dto = dto
        }, cancellationToken);
    }
}
