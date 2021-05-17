using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities.Abstracts;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Artisan.Application.Commands.Items.DeleteItem
{
    public class DeleteItemCommandHandler : AbstractRequestHandler<DeleteItemCommand>
    {
        private readonly Context _context;

        public DeleteItemCommandHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            Item item = await _context.Items.SingleOrDefaultAsync(
                m => m.Id == request.ItemId && m.ShopId == request.ShopId, cancellationToken);
            if (item == null)
                return NotFound<Item>(request.ItemId);
            
            if (item.Name != request.Name)
                return BadRequest("Name doesn't match");

            _context.Items.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}