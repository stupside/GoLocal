using System;

namespace GoLocal.Core.Client.Application.Queries.Invoices.GetInvoice.Models
{
    public class InvoiceItemDto
    {
        public string Id { get; init; }
        
        public int Quantity { get; init; }
        public float Price { get; init; }
        public string Description { get; init; }
        public ItemDto Item { get; init; }
        public DateTime Creation { get; init; }
    }
}