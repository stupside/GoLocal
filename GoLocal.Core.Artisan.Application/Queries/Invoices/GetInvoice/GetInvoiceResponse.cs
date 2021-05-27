using System;
using System.Collections.Generic;
using GoLocal.Core.Artisan.Application.Queries.Invoices.GetInvoice.Models;

namespace GoLocal.Core.Artisan.Application.Queries.Invoices.GetInvoice
{
    public class GetInvoiceResponse
    {
        public int Id { get; set; }

        public ShopDto Shop { get; init; }
        public ICollection<InvoiceItemDto> InvoiceItems { get; init; }
        public UserDto User { get; init; }
        
        public DateTime Creation { get; }
    }
}