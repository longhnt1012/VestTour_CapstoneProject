namespace VestTour.Domain.Entities
{
    public class ProductStyleOption
    {
        public int ProductId { get; set; }
        public int StyleOptionId { get; set; }

        public Product Product { get; set; }
        public StyleOption StyleOption { get; set; }
    }
}
