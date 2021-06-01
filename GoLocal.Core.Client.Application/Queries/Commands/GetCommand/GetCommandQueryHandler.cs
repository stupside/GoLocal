using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;

namespace GoLocal.Core.Client.Application.Queries.Commands.GetCommand
{
    public class GetCommandQueryHandler : AbstractRequestHandler<GetCommandQuery, GetCommandResponse>
    {
        public override Task<Result<GetCommandResponse>> Handle(GetCommandQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}