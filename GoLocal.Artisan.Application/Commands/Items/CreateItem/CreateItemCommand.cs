using System;
using GoLocal.Domain.Entities.Abstracts;
using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Commands.Items.CreateItem
{
    public class CreateItemCommand : AbstractRequest<int>
    {
        public int ShopId { get; init; }
        
        public string Name { get; init; }
        public string Description { get; init; }
        internal Type Type { get; private set; }
        
        public void SetItemType<TItem>()
            where TItem : Item
        {
            Type = typeof(TItem);
        }
    }
}