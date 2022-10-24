using System;
using System.Collections.Generic;
using System.Text;

namespace ZaloDotNetSDK.entities.shop
{
    public class Order
    { 
        public Order(Customer customer, string extra_note, List<OrderItem> order_items)
        {
            Customer = customer;
            Extra_note = extra_note;
            Order_items = order_items;
        }

        public Order()
        {
        } 
        public Customer Customer { get; set; }
        public string Extra_note { get; set; }
        public List<OrderItem> Order_items { get; set; }
    }
}
