namespace VestTour.Repository.Models
{
    public class StoreModel
    {
        public int StoreId { get; set; }               // Corresponds to StoreID in the database
        public int UserId { get; set; }                // Foreign Key to User table
        public string Name { get; set; }               // Name of the store
        public string? Address { get; set; }           // Address of the store (nullable)
        public string? ContactNumber { get; set; }     // Contact number of the store (nullable)
    }
}
