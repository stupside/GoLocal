using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results.Pages;
using GoLocal.Core.Artisan.Application.Queries.Commands.GetCommands.Models;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Queries.Commands.GetCommands
{
    [AuthorizedEntity(typeof(Shop))]
    public class GetCommandsQuery : AbstractPagedRequest<Command, CommandDto>
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }

        protected override void ConfigurePaging(PageRequestConfiguration<Command> paging)
        {
            paging.MapFor("id", command => command.Id);
        }
    }
}