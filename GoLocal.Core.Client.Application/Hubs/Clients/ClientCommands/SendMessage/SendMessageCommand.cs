using MediatR;

namespace GoLocal.Core.Client.Application.Hubs.Clients.ClientCommands.SendMessage
{
    public class SendMessageCommand : INotification
    {
        public int ShopId { get; init; }
        public string CommandId { get; init; }
        public string Body { get; init; }

        public SendMessageCommand()
        {
        }

        public SendMessageCommand(int shopId, string commandId, string body)
        {
            ShopId = shopId;
            CommandId = commandId;
            Body = body;
        }
    }
}