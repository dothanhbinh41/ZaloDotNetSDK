namespace ZaloDotNetSDK.entities.shop
{
    public struct ShopStatus
    {
        public string Value { get; }

        private ShopStatus(string value)
        {
            Value = value;
        }

        public static ShopStatus SHOW = new ShopStatus("show");
        public static ShopStatus HIDE = new ShopStatus("hide");
    }
}