namespace ZaloDotNetSDK.entities.shop
{
    public class PackageSize
    {
        private double weight;
        private double length;
        private double width;
        private double height;

        public PackageSize(double weight, double length, double width, double height)
        {
            Weight = weight;
            Length = length;
            Width = width;
            Height = height;
        }

        public PackageSize()
        {
        }

        public double Weight { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}