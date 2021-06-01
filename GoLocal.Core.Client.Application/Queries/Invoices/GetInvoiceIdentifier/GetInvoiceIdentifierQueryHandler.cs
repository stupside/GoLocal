using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results;
using GoLocal.Core.Domain.Enums;
using GoLocal.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;
using QRCoder;

namespace GoLocal.Core.Client.Application.Queries.Invoices.GetInvoiceIdentifier
{
    public class GetInvoiceIdentifierQueryHandler : AbstractRequestHandler<GetInvoiceIdentifierQuery, GetInvoiceIdentifierResponse>
    {
        private readonly Context _context;

        public GetInvoiceIdentifierQueryHandler(Context context)
        {
            _context = context;
        }

        public override async Task<Result<GetInvoiceIdentifierResponse>> Handle(GetInvoiceIdentifierQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _context.Invoices
                .Where(m => m.Id == request.InvoiceId).Select(m => new
                {
                    m.Code,
                    m.Id,
                    m.Status
                }).SingleOrDefaultAsync(cancellationToken);

            if (invoice.Status is not InvoiceStatus.Ready)
                return BadRequest(
                    "You can't get your invoice identifier yet. Please wait the artisan to update the status of your invoice");
            
            var code = new QRCodeGenerator().CreateQrCode(invoice.Code, QRCodeGenerator.ECCLevel.Q);
            GetInvoiceIdentifierResponse response = new GetInvoiceIdentifierResponse
            {
                Code = invoice.Code,
                QrCode = new Base64QRCode(code).GetGraphic(20)
            };
            
            return Ok(response);
        }
    }
}