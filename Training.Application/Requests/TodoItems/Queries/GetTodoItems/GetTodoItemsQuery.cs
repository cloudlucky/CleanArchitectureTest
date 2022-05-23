using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Application.Common.Data;
using Training.Domain;

namespace Training.Application.Requests.TodoItems.Queries.GetTodoItems;
public class GetTodoItemsQuery : IRequest<GetTodoItemsQueryViewModel>
{

    internal class Handler : IRequestHandler<GetTodoItemsQuery, GetTodoItemsQueryViewModel>
    {
        private readonly IApplicationDbContext db;

        public Handler(IApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<GetTodoItemsQueryViewModel> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
        {
            var result = await this.GetTodoItemsAsync(cancellationToken);
            var dto = MapToDto(result);

            return new GetTodoItemsQueryViewModel
            {
                Items = dto
            };
        }

        private async Task<List<TodoItem>> GetTodoItemsAsync(CancellationToken cancellationToken)
        {
            return await this.db.TodoItems
                                .ToListAsync(cancellationToken);
        }

        private static List<TodoItemDto> MapToDto(IEnumerable<TodoItem> todoItems)
        {
            return todoItems
                .Select(x => new TodoItemDto
                {
                    Id = x.Id,
                    IsCompleted = x.IsCompleted,
                    Text = x.Text
                })
                .ToList();
        }
    }
}
