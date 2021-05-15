using System;
using GoLocal.Shared.Bus.Commons.Mediator;

namespace GoLocal.Artisan.Application.Commands.Items.CreateItem
{
    public class CreateItemCommand : AbstractRequest<string>
    {
        public int ShopId { get; init; }
        
        public string Name { get; init; }
        public string Description { get; init; }
        internal Type Type { get; set; }

        public void SetType<TItem>()
        {
            Type = typeof(TItem);
        }
    }
}