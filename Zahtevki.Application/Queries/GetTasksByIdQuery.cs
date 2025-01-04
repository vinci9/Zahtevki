using MediatR;
using Zahtevki.Core.Entities;
using Zahtevki.Core.Interfaces;

namespace Zahtevki.Application.Queries
{
    public record GetTasksByIdQuery(Guid Id) : IRequest<TasksEntity>;
    public class GetTasksByIdQueryHandler(ITasksRepository tasksRepository) : IRequestHandler<GetTasksByIdQuery, TasksEntity>
    {
        public async Task<TasksEntity> Handle(GetTasksByIdQuery request, CancellationToken cancellationToken)
        {
            return await tasksRepository.GetTaskByIdAsync(request.Id);
        }
    }
}