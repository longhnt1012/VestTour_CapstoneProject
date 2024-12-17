namespace VestTour.Repository.Models
{
    public class StyleOptionModel
    {
        public int StyleOptionId { get; set; }           // Corresponds to StyleOptionID in the database
        public int StyleId { get; set; }                  // Corresponds to StyleID in the database
        public string? OptionType { get; set; }           // Type of the option
        public string? OptionValue { get; set; }
        public decimal? Price { get; set; }// Value of the option
        public string? Status { get; set; }
    }
}
