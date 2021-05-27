using System;
using GoLocal.Bus.Authorizer.Authorizers.Attributes;
using GoLocal.Bus.Commons.Mediator;
using GoLocal.Core.Domain.Entities;
using GoLocal.Core.Domain.Entities.Abstracts;

namespace GoLocal.Core.Artisan.Application.Commands.Items.CreateItem
{
    [AuthorizedEntity(typeof(Shop))]
    public class CreateItemCommand : AbstractRequest<int>
    {
        [AuthorizedEntityId]
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