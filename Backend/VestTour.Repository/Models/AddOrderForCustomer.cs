using VestTour.Repository.Models;

public class AddOrderForCustomer
{
    public int? UserID { get; set; }
    public int? StoreId { get; set; }
    public int? VoucherId { get; set; }
    public DateOnly? ShippedDate { get; set; }
    public string? Note { get; set; }
    public bool Paid { get; set; }
    public string? GuestName { get; set; }
    public string? GuestEmail { get; set; }
    public string? GuestAddress { get; set; }
    public string? GuestPhone { get; set; }
    public decimal? Deposit { get; set; }
    public decimal? ShippingFee { get; set; }
    public string? DeliveryMethod { get; set; }
    public List<BuyProductModel>? Products { get; set; } = new();
    public List<CustomProductModel>? CustomProducts { get; set; } = new(); // List of custom products
}
