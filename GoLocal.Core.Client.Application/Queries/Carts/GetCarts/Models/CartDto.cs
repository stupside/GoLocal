using System;
using System.Collections.Generic;

namespace GoLocal.Core.Client.Application.Queries.Carts.GetCarts.Models
{
    public class CartDto
    {
        public ShopDto Shop { get; init; }
        public IEnumerable<CartPackageDto> CartPackages { get; init; }
        public DateTime Creation { get; init; }
    }
}