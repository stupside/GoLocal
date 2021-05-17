using System.Threading;
using System.Threading.Tasks;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;

namespace GoLocal.Client.Application.Commands.Carts.RemoveCartPackage
{
    public class RemoveCartPackageCommandHandler : AbstractRequestHandler<RemoveCartPackageCommand>
    {
        public override Task<Result> Handle(RemoveCartPackageCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}