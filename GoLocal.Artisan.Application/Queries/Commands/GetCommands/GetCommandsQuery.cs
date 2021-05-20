using GoLocal.Artisan.Application.Queries.Commands.GetCommands.Models;
using GoLocal.Domain.Entities;
using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Queries.Commands.GetCommands
{
    public class GetCommandsQuery : AbstractPagedRequest<Command, CommandDto>
    {
        public int ShopId { get; init; }
        public GetCommandsQuery()
        {
            this.ConfigureSearch(m =>
                m.MapFor("id", command => command.Id));

            this.ConfigureOrder(m =>
                m.MapFor("id", command => command.Id));
            
        }
    }
}