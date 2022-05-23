using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Application.Common.Data;
using Training.Domain;

namespace Training.Application.Requests.TodoItems.Queries.GetTodoItemById;
public class GetTodoItemByIdQuery : IRequest<GetTodoItemByIdQueryViewModel>
{
    public Guid Id { get; set; }

    internal class Handler : IRequestHandler<GetTodoItemByIdQuery, GetTodoItemByIdQueryViewModel>
    {
        private readonly IApplicationDbContext db;

        public Handler(IApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<GetTodoItemByIdQueryViewModel> Handle(GetTodoItemByIdQuery request, CancellationToken cancellationToken)
        {
            var todoItem = await this.GetAsync(request.Id, cancellationToken);

            var dto = new TodoItemDto
            {
                Id = todoItem.Id,
                IsCompleted = todoItem.IsCompleted,
                Text = todoItem.Text
            };

            return new GetTodoItemByIdQueryViewModel
            {
                Dto = dto
            };
        }

        private async Task<TodoItem> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await this.db.TodoItems.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (result is null)
            {
                throw new InvalidOperationException();
            }

            return result;
        }
    }
}
