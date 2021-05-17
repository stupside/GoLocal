using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities.Abstracts;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Artisan.Application.Commands.Items.UpdateItem
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
            
            bool nameTaken = await _context.Items.AnyAsync(m => m.Name == request.NewName && m.ShopId == request.ShopId, cancellationToken);
            if (nameTaken)
                return BadRequest($"An item named {request.NewName} already exists");

            item.Name = request.NewName;
            item.Description = request.Description;

            _context.Items.Update(item);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}