using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities.Abstracts;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Artisan.Application.Commands.Items.UpdateItemDescription
{
    public class UpdateItemDescriptionCommandHandler : AbstractRequestHandler<UpdateItemDescriptionCommand>
    {
        private readonly Context _context;

        public UpdateItemDescriptionCommandHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result> Handle(UpdateItemDescriptionCommand request, CancellationToken cancellationToken)
        {
            Item item = await _context.Items.SingleOrDefaultAsync(
                m => m.Id == request.ItemId && m.ShopId == request.ShopId, cancellationToken);
            if (item == null)
                return NotFound<Item>(request.ItemId);

            item.Description = request.Description;
            
            _context.Items.Update(item);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}