using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Artisan.Application.Commons.Helpers;
using GoLocal.Core.Domain.Entities.Abstracts;
using GoLocal.Core.Domain.Enums;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Artisan.Application.Commands.Items.UpdateItemImage
{
    public class UpdateItemImageCommandHandler : AbstractRequestHandler<UpdateItemImageCommand>
    {
        private readonly Context _context;

        public UpdateItemImageCommandHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result> Handle(UpdateItemImageCommand request, CancellationToken cancellationToken)
        {
            Item item = await _context.Items.SingleOrDefaultAsync(
                m => m.Id == request.ItemId && m.ShopId == request.ShopId, cancellationToken);
            if (item == null)
                return NotFound<Item>(request.ItemId);

            item.Image = await request.File.ResizeAsync();
            _context.Items.Update(item);
            await _context.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}