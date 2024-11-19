﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Service.Interface;
using VestTour.Repository.Models;
using VestTour.Service.Interface;
using VestTour.Service.Interfaces;

namespace VestTour.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInStoreController : ControllerBase
    {
        private readonly IProductInStoreService _productInStoreService;

        public ProductInStoreController(IProductInStoreService productInStoreService)
        {
            _productInStoreService = productInStoreService;
        }

        [HttpGet("{storeId}/{productId}")]
        public async Task<ActionResult<ServiceResponse<ProductInStoreModel?>>> GetProductInStore(int storeId, int productId)
        {
            var response = await _productInStoreService.GetProductInStoreAsync(storeId, productId);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<ProductInStoreModel>>>> GetAllProductsInStore()
        {
            var response = await _productInStoreService.GetAllProductsInStoreAsync();
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<int>>> AddProductInStore(ProductInStoreModel productInStore)
        {
            var response = await _productInStoreService.AddProductInStoreAsync(productInStore);
            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetProductInStore), new { storeId = productInStore.StoreID, productId = response.Data }, response);
        }

        //[HttpPut("{storeId}/{productId}")]
        //public async Task<ActionResult<ServiceResponse>> UpdateProductInStore(int storeId, int productId, ProductInStoreModel productInStore)
        //{
        //    var response = await _productInStoreService.UpdateProductInStoreAsync(storeId, productId, productInStore);
        //    if (!response.Success)
        //        return BadRequest(response);

        //    return Ok(response);
        //}

        [HttpDelete("{storeId}/{productId}")]
        public async Task<ActionResult<ServiceResponse>> DeleteProductInStore(int storeId, int productId)
        {
            var response = await _productInStoreService.DeleteProductInStoreAsync(storeId, productId);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPatch("{storeId}/{productId}/quantity")]
        public async Task<ActionResult<ServiceResponse>> UpdateQuantity(int storeId, int productId, [FromBody] int quantity)
        {
            var response = await _productInStoreService.UpdateQuantityAsync(storeId, productId, quantity);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}