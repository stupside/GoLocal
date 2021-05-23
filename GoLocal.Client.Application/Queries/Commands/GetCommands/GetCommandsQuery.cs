using GoLocal.Client.Application.Queries.Commands.GetCommands.Models;
using GoLocal.Domain.Entities;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results.Pages;

namespace GoLocal.Client.Application.Queries.Commands.GetCommands
{
    public class GetCommandsQuery : AbstractPagedRequest<Command, CommandDto>
    {
        
        protected override void ConfigurePaging(PageRequestConfiguration<Command> paging)
        {
            paging.MapFor("sid", m => m.Package.Item.ShopId);
        }
    }
}