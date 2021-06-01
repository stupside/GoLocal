using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results.Pages;
using GoLocal.Core.Client.Application.Queries.Commands.GetCommands.Models;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Client.Application.Queries.Commands.GetCommands
{
    public class GetCommandsQuery : AbstractPagedRequest<Command, CommandDto>
    {
        protected override void ConfigurePaging(PageRequestConfiguration<Command> paging)
        {
            paging.MapFor("sid", m => m.Package.Item.ShopId);
        }
    }
}