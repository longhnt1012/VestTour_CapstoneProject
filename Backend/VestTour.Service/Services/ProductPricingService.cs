using VestTour.Repository.Interface;
using VestTour.Repository.Models;

public class ProductPricingService
{
    private readonly IFabricRepository _fabricRepository;
    private readonly IStyleOptionRepository _styleOptionRepository;

    public ProductPricingService(IFabricRepository fabricRepository, IStyleOptionRepository styleOptionRepository)
    {
        _fabricRepository = fabricRepository;
        _styleOptionRepository = styleOptionRepository;
    }

    public async Task<decimal> CalculatePriceAsync(CustomProductModel product)
    {
        decimal fabricPrice = await GetFabricPriceAsync(product.FabricID);
        decimal styleOptionsPrice = await GetTotalStyleOptionsPriceAsync(product.PickedStyleOptions);

        return fabricPrice + styleOptionsPrice;
    }

    private async Task<decimal> GetFabricPriceAsync(int? fabricId)
    {
        if (fabricId.HasValue)
        {
            var fabricPrice = await _fabricRepository.GetFabricPriceByIdAsync(fabricId.Value);
            return fabricPrice ?? 0;
        }
        return 0;
    }

    private async Task<decimal> GetTotalStyleOptionsPriceAsync(List<PickedStyleOptionModel> pickedStyleOptions)
    {
        decimal totalStyleOptionPrice = 0;

        foreach (var option in pickedStyleOptions)
        {
            var price = await _styleOptionRepository.GetStyleOptionPriceByIdAsync(option.StyleOptionID);
            totalStyleOptionPrice += price ?? 0;
        }

        return totalStyleOptionPrice;
    }
}
