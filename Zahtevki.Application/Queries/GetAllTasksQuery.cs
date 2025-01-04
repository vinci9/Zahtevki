using MediatR;
using Zahtevki.Core.Entities;
using Zahtevki.Core.Interfaces;

namespace Zahtevki.Application.Queries
{
    public record GetAllTasksQuery() : IRequest<IEnumerable<TasksEntity>>;
    public class GetAllTasksQueryHandler(ITasksRepository tasksRepository) : IRequestHandler<GetAllTasksQuery, IEnumerable<TasksEntity>>
    {
        public async Task<IEnumerable<TasksEntity>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            return await tasksRepository.GetAllTasks();
        }
    }
}