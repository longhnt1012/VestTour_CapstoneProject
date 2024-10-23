
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using VestTour.Repository.Models;

public class CustomProductModel
{
   
    public string ProductCode { get; private set; } // Set to private set to prevent external modification

    public int CategoryID { get; set; }
    public int FabricID { get; set; }
    public int? LiningID { get; set; }
    public int? MeasurementID { get; set; }
    public List<PickedStyleOptionModel> PickedStyleOptions { get; set; } = new List<PickedStyleOptionModel>();

    // Constructor to generate ProductCode
    public CustomProductModel()
    {
        ProductCode = GenerateProductCode();
    }

    private string GenerateProductCode()
    {
        Random random = new Random();
        int randomNumber = random.Next(100, 1000); // Generates a number between 100 and 999
        return $"SUIT{randomNumber}";
    }
}
