using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Application.Common.Data;
using Training.Domain;

namespace Training.Application.Requests.TodoItems.Commands.UpdateTodoItem;
public class UpdateTodoItemCommand : IRequest
{
    public Guid Id { get; set; }

    public TodoItemDto Dto { get; set; } = default!;

    internal class Handler : IRequestHandler<UpdateTodoItemCommand>
    {
        private readonly IApplicationDbContext db;

        public Handler(IApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<Unit> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = await this.GetAsync(request.Id, cancellationToken);
            Update(request.Dto, todoItem);

            await this.db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task<TodoItem> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var todoItem = await this.db.TodoItems.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (todoItem is null)
            {
                throw new InvalidOperationException();
            }

            return todoItem;
        }

        private static void Update(TodoItemDto dto, TodoItem todoItem)
        {
            todoItem.Text = dto.Text;
            todoItem.IsCompleted = dto.IsCompleted;
        }
    }
}
