using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;

public class CustomProductModel
{
    public string? ProductCode { get;  set; }
    public int CategoryID { get; set; }
    public int FabricID { get; set; }
    public int? LiningID { get; set; }
    public int? MeasurementID { get; set; }
    public int Quantity { get; set; }
    public List<PickedStyleOptionModel> PickedStyleOptions { get; set; } = new List<PickedStyleOptionModel>();
    public CustomProductModel() {
        
    }


    public async Task<string> GenerateProductCodeAsync(IFabricRepository fabricRepository)
    {
        try
        {
            string fabricName = await fabricRepository.GetFabricNameByIdAsync(FabricID);
            if (string.IsNullOrEmpty(fabricName))
            {
                throw new InvalidOperationException("Fabric name not found for the provided FabricID.");
            }

            string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            return $"SUIT{fabricName}{currentDateTime}";
        }
        catch (Exception ex)
        {
            // Log exception here
            throw new ApplicationException("An error occurred while generating the product code.", ex);
        }
    }


    public void SetProductCode(string code)
    {
        ProductCode = code;
    }
}
