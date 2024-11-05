namespace VestTour.Domain.Entities
{
    public class ProductStyleOption
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }  // Navigation property

        public int StyleOptionId { get; set; }
        public StyleOption StyleOption { get; set; }  // Navigation property
    }

}
