using System.Collections.Generic;

namespace ZaloDotNetSDK.entities.shop
{
    public class Variation
    { 
        public Variation(int variationDefault, PackageSize packageSize, List<string> attributes, int quantity, ShopStatus status, string code, string name, long price)
        {
            VariationDefault = variationDefault;
            PackageSize = packageSize;
            Attributes = attributes;
            Quantity = quantity;
            Status = status;
            Code = code;
            Name = name;
            Price = price;
        }

        public Variation()
        {
        }

        public int VariationDefault { get; set; }
        public PackageSize PackageSize { get; set; }
        public List<string> Attributes { get; set; }
        public int Quantity { get; set; }
        public ShopStatus Status { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
    }
}