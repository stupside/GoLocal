using System.Threading;
using System.Threading.Tasks;
using System.Web;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Enums;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Core.Artisan.Application.Commands.Invoices.MakeInvoicePicked
{
    public class MakeInvoicePickedCommandHandler : AbstractRequestHandler<MakeInvoicePickedCommand>
    {
        private readonly Context _context;

        public MakeInvoicePickedCommandHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result> Handle(MakeInvoicePickedCommand request, CancellationToken cancellationToken)
        {
            string[] decomposed = HttpUtility.UrlDecode(request.Identifier).Split("#");
            
            if (string.IsNullOrEmpty(decomposed[0]) || string.IsNullOrEmpty(decomposed[1]))
                return BadRequest("Invalid invoice identifier");

            if (!int.TryParse(decomposed[0], out int id))
                return BadRequest("Invalid invoice identifier");
            
            Invoice invoice = await _context.Invoices.SingleOrDefaultAsync(m => m.Id == id && m.ShopId == request.ShopId && m.Code == decomposed[1], cancellationToken);

            if(invoice == null)
                return BadRequest("Invalid invoice identifier");

            if (invoice.Status is not InvoiceStatus.Ready)
                return BadRequest(
                    $"You can't change the status of this invoice to picked because the status was set to '{invoice.Status}'");

            invoice.Status = InvoiceStatus.Picked;

            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Ok();
        }
    }
}