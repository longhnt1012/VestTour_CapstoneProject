namespace VestTour.Repository.Models
{
    public class StyleModel
    {
        public int StyleId { get; set; }          // Corresponds to StyleID in the database
        public string? StyleName { get; set; }    // Name of the style
        public string? Status { get; set; }
    }
}
