using GoLocal.Artisan.Application.Queries.Commands.GetCommands.Models;
using GoLocal.Domain.Entities;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results.Pages;

namespace GoLocal.Artisan.Application.Queries.Commands.GetCommands
{
    public class GetCommandsQuery : AbstractPagedRequest<Command, CommandDto>
    {
        public int ShopId { get; init; }

        protected override void ConfigurePaging(PageRequestConfiguration<Command> paging)
        {
            paging.MapFor("id", command => command.Id);
        }
    }
}