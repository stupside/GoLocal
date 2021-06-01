using System;
using System.Collections.Generic;
using GoLocal.Core.Artisan.Application.Queries.Invoices.GetInvoice.Models;
using GoLocal.Core.Domain.Enums;

namespace GoLocal.Core.Artisan.Application.Queries.Invoices.GetInvoice
{
    public class GetInvoiceResponse
    {
        public int Id { get; init; }
        public InvoiceStatus Status { get; init; }
        public IEnumerable<InvoiceItemDto> InvoiceItems { get; init; }
        public UserDto User { get; init; }
        
        public DateTime Creation { get; init; }
    }
}