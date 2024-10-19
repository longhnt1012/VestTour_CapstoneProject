namespace VestTour.Repository.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }

        public int? CategoryParentId { get; set; }

        public string? Name { get; set; }

        public string? ImageUrl { get; set; }

        public string? Description { get; set; }
    }
}
