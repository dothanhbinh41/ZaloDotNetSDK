using System;
using System.Collections.Generic;
using System.Text;

namespace ZaloDotNetSDK.entities.shop
{
    public class Product
    { 
        public Product(List<Variation> variations, PackageSize packageSize, ShopStatus status, List<string> photos, List<string> categories, int quantity, long price, string code, string description, string name, int industry, string id)
        {
            Variations = variations;
            PackageSize = packageSize;
            Status = status;
            Photos = photos;
            Categories = categories;
            Quantity = quantity;
            Price = price;
            Code = code;
            Description = description;
            Name = name;
            Industry = industry;
            Id = id;
        }

        public Product()
        {
        }

        public List<Variation> Variations { get; set; }
        public PackageSize PackageSize { get; set; }
        public ShopStatus Status { get; set; }
        public List<string> Photos { get; set; }
        public List<string> Categories { get; set; }
        public int Quantity { get; set; }
        public long Price { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int Industry { get; set; }
        public string Id { get; set; }
    }
}
