namespace VestTour.Repository.Models
{
    public class StoreModel
    {
        public int StoreId { get; set; }              
        public int UserId { get; set; }            
        public string Name { get; set; }           
        public string? Address { get; set; }        
        public string? ContactNumber { get; set; }
        public int? StoreCode { get; set; }
        public TimeOnly? OpenTime { get; set; }
        public TimeOnly? CloseTime { get; set; }
        public string? StaffIDs { get; set; }
        public int? DistrictID { get; set; }
    }
}
