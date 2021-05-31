using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Accessors;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Entities.Identity;
using GoLocal.Core.Domain.Enums;
using GoLocal.Core.Persistence.EntityFramework;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Client.Application.Hubs.Clients.ClientCommands.SendMessage
{
    public class SendMessageCommandHandler : INotificationHandler<SendMessageCommand>
    {
        private readonly Context _context;
        private readonly IUserAccessor<User> _accessor;
        private readonly IHubContext<CommandHub, ICommandClient> _hub;

        public SendMessageCommandHandler(IHubContext<CommandHub, ICommandClient> hub, IUserAccessor<User> accessor, Context context)
        {
            _hub = hub;
            _accessor = accessor;
            _context = context;
        }

        public async Task Handle(SendMessageCommand notification, CancellationToken cancellationToken)
        {
            User user = await _accessor.GetUserAsync();
            
            var command = await _context.Commands
                .Where(m => m.Id == notification.CommandId)
                .Select(m => new { m.Id, m.UserId, m.ShopId, OwnerId = m.Shop.UserId, m.Status  })
                .SingleOrDefaultAsync(cancellationToken);

            if(command?.Status is not CommandStatus.Accepted)
                return;

            Message message = new Message(command.Id, user.Id, notification.Body);
            
            await _context.Messages.AddAsync(message, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            await _hub.Clients.Users(command.UserId, command.OwnerId).ReceiveMessage(notification.CommandId, notification.Body);
        }
    }
}