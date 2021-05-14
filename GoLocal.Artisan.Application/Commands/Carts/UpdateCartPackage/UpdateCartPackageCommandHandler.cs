using System.Threading;
using System.Threading.Tasks;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using MediatR;

namespace GoLocal.Artisan.Application.Commands.Carts.UpdateCartPackage
{
    public class UpdateCartPackageCommandHandler : AbstractRequestHandler<UpdateCartPackageCommand>
    {
        public override Task<Result<Unit>> Handle(UpdateCartPackageCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
            // Check si la quantité qu'on ajoute est <= au stock disponible
            // Check si un panier est déja crée => si déja crée -> le produit est déja dans le panier -> si update Quantity
            // Dans tout les casS i pas dans le panier -> créer panier, ajouter au panier
        }
    }
}