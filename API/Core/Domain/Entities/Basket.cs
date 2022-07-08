using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Core.Domain.Entities
{
    public class Basket : Entity
    {
        public Guid BuyerId { get; set; }

        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

        public void AddItem(Product product, int quantity)
        {
            if (Items.All(item => item.ProductId != product.Id))
            {
                Items.Add(new BasketItem{BasketId = Id, ProductId = product.Id, Product = product, Quantity = quantity});
            }

            var existingItem = Items.FirstOrDefault(item => item.ProductId == product.Id);
            if (existingItem != null) existingItem.Quantity += quantity;
        }

        public void RemoveItem(Guid productId, int quantity)
        {
            var item = Items.FirstOrDefault(item => item.ProductId == productId);
            if (item == null) return;
            item.Quantity -= quantity;
            if (item.Quantity == 0) Items.Remove(item);
        }
    }
}