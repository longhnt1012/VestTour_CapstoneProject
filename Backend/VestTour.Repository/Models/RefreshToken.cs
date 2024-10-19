namespace VestTour.Repository.Models
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
        public int UserId { get; set; }
    }
}
