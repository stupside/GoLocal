using MediatR;

namespace GoLocal.Core.Client.Application.Hubs.Clients.ClientCommands.GetHistory
{
    public class GetHistoryQuery : INotification
    {
        public int ShopId { get; init; }
        public string CommandId { get; init; }
        public int Skip { get; init; }
        
        public GetHistoryQuery()
        {
        }

        public GetHistoryQuery(int shopId, string commandId, int skip)
        {
            ShopId = shopId;
            CommandId = commandId;
            Skip = skip;
        }
    }
}