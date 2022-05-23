using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Application.Common.Data;
using Training.Domain;

namespace Training.Application.Requests.TodoItems.Commands.CreateTodoItem;
public class CreateTodoItemCommand : IRequest
{
    public TodoItemDto TodoItemDto { get; set; } = default!;

    public class Validation : AbstractValidator<CreateTodoItemCommand>
    {
        public Validation()
        {
            this.RuleFor(x => x.TodoItemDto.Text).MinimumLength(1);
        }
    }

    internal class Handler : IRequestHandler<CreateTodoItemCommand>
    {
        private readonly IApplicationDbContext db;

        public Handler(IApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<Unit> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = new TodoItem
            {
                Text = request.TodoItemDto.Text,
                IsCompleted = request.TodoItemDto.IsCompleted
            };

            this.db.TodoItems.Add(todoItem);

            await this.db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
