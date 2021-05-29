using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities.Abstracts;
using GoLocal.Core.Domain.Enums;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Artisan.Application.Commands.Items.UpdateItem
{
    public class UpdateItemCommandHandler : AbstractRequestHandler<UpdateItemCommand>
    {
        private readonly Context _context;

        public UpdateItemCommandHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            Item item = await _context.Items.SingleOrDefaultAsync(
                m => m.Id == request.ItemId && m.ShopId == request.ShopId, cancellationToken);
            if (item == null)
                return NotFound<Item>(request.ItemId);
            
            if (item.Name != request.OldName)
                return BadRequest("Old name doesn't match");

            if (item.Name == request.NewName)
                return Ok();
            
            if (await _context.Items.AnyAsync(m => m.Name == request.NewName && m.ShopId == request.ShopId, cancellationToken))
                return BadRequest($"An item named {request.NewName} already exists");

            item.Name = request.NewName;

            _context.Items.Update(item);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}