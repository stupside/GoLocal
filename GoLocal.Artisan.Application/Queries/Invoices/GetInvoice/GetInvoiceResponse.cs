using System;
using System.Collections.Generic;
using GoLocal.Artisan.Application.Queries.Invoices.GetInvoice.Models;
using GoLocal.Domain.Entities;

namespace GoLocal.Artisan.Application.Queries.Invoices.GetInvoice
{
    public class GetInvoiceResponse
    {
        public int Id { get; set; }

        public Shop Shop { get; init; }
        public ICollection<InvoiceItemDto> InvoiceItems { get; init; }
        public UserDto User { get; init; }
        
        public DateTime Creation { get; }
    }
}