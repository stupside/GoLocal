using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Queries.Commands.GetCommand
{
    public class GetCommandQuery : AbstractRequest
    {
        public string Id { get; init; }
        public int ShopId { get; init; }
        public string UserId { get; init; }
        
    }
}