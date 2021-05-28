using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities.Abstracts;
using GoLocal.Core.Domain.Enums;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Artisan.Application.Commands.Items.DeleteItem
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
                m => m.Id == request.ItemId && m.ShopId == request.ShopId && m.Visibility != Visibility.Deleted, cancellationToken);
            if (item == null)
                return NotFound<Item>(request.ItemId);
            
            if (item.Name != request.Name)
                return BadRequest("Name doesn't match");

            item.Visibility = Visibility.Deleted;
            
            _context.Items.Update(item);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}