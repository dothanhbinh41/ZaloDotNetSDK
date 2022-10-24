using System;
using System.Collections.Generic;
using System.Text;

namespace ZaloDotNetSDK.entities.shop
{
    public class Customer
    {

        public Customer(string name, string phone, string user_id, string address, int dictrict, int city)
        {
            Name = name;
            Phone = phone;
            UserId = user_id;
            Address = address;
            Dictrict = dictrict;
            City = city;
        }

        public Customer()
        {
        }

        public string Name { set; get; }
        public string Phone { set; get; }
        public string UserId { set; get; }
        public string Address { set; get; }
        public int Dictrict { set; get; }
        public int City { set; get; }
    }
}
