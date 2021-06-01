using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Core.Client.Application.Hubs.Clients.ClientCommands.GetHistory.Models;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Persistence.EntityFramework;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Client.Application.Hubs.Clients.ClientCommands.GetHistory
{
    public class GetHistoryQueryHandler : INotificationHandler<GetHistoryQuery>
    {
        private readonly Context _context;
        private readonly IUserAccessor<User> _accessor;
        private readonly IHubContext<CommandHub, ICommandClient> _hub;

        public GetHistoryQueryHandler(Context context, IUserAccessor<User> accessor, IHubContext<CommandHub, ICommandClient> hub)
        {
            _context = context;
            _accessor = accessor;
            _hub = hub;
        }

        public async Task Handle(GetHistoryQuery notification, CancellationToken cancellationToken)
        {
            User user = await _accessor.GetUserAsync();
            if(await _context.Commands.AnyAsync(m => m.Id == notification.CommandId && m.UserId == user.Id || (m.Shop.UserId == user.Id), cancellationToken))
                return;

            List<MessageDto> messages = await _context.Messages
                .Where(m => m.CommandId == notification.CommandId)
                .OrderByDescending(m => m.Creation)
                .Skip(notification.Skip)
                .ProjectToType<MessageDto>()
                .ToListAsync(cancellationToken);

            await _hub.Clients.User(user.Id).ReceiveHistory(messages);
        }
    }
}