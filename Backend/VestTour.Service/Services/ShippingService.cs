using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestTour.Repository.Common;
using VestTour.Repository.Models;
using VestTour.Repository.Models.ShippingModel;

namespace VestTour.Service.Services
{
    public class ShippingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiToken;

        public ShippingService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiToken = configuration["ShippingService:ApiToken"];
        }

        public async Task<List<ProvinceModel>> GetProvincesAsync()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://online-gateway.ghn.vn/shiip/public-api/master-data/province"),
                Headers =
            {
                { "Token", _apiToken },
                { "Accept", "application/json" }
            }
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<ProvinceModel>>>(responseBody);

            return apiResponse.Data;
        }

        public async Task<List<DistrictModel>> GetDistrictsAsync(int provinceId)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://online-gateway.ghn.vn/shiip/public-api/master-data/district"),
                Headers =
            {
                { "Token", _apiToken },
                { "Accept", "application/json" }
            },
                Content = new StringContent(JsonConvert.SerializeObject(new { province_id = provinceId }), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<DistrictModel>>>(responseBody);

            return apiResponse.Data;
        }
        public async Task<List<WardModel>> GetWardsAsync(int districtId)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://online-gateway.ghn.vn/shiip/public-api/master-data/ward"),
                Headers =
        {
            { "Token", _apiToken },
            { "Accept", "application/json" }
        },
                Content = new StringContent(JsonConvert.SerializeObject(new { district_id = districtId }), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<WardModel>>>(responseBody);

            return apiResponse.Data;
        }

        public async Task<List<ShippingServiceModel>> GetAvailableServicesAsync(int shopId, int fromDistrict, int toDistrict)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/available-services"),
                Headers =
        {
            { "Token", _apiToken },
            { "Accept", "application/json" }
        },
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    shop_id = shopId,
                    from_district = fromDistrict,
                    to_district = toDistrict
                }), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<ShippingServiceModel>>>(responseBody);

            return apiResponse.Data;
        }

    }

}
