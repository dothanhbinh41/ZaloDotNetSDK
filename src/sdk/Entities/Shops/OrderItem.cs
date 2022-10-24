using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZaloDotNetSDK.entities.shop
{
    public class OrderItem
    { 
        public OrderItem(string product_id, int quantity, string variation_id, string partner_item_data)
        {
            ProductId = product_id;
            Quantity = quantity;
            VariationId = variation_id;
            PartnerItemData = partner_item_data;
        }

        public OrderItem()
        {
        }

        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public string PartnerItemData { get; set; }
        public string VariationId { get; set; }
    }
}
