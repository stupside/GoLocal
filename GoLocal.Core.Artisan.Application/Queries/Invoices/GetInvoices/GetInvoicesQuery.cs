using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Bus.Results.Pages;
using GoLocal.Core.Artisan.Application.Queries.Invoices.GetInvoices.Models;
using GoLocal.Core.Domain.Entities;

namespace GoLocal.Core.Artisan.Application.Queries.Invoices.GetInvoices
{
    [AuthorizedEntity(typeof(Shop))]
    public class GetInvoicesQuery : AbstractPagedRequest<Invoice, InvoiceDto>
    {
        [AuthorizedEntityId]
        public int ShopId { get; init; }
        
        protected override void ConfigurePaging(PageRequestConfiguration<Invoice> paging)
        {
            paging.MapFor("status", m => m.Status);
        }
    }
}