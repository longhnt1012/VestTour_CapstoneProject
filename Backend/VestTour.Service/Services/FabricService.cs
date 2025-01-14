﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Domain.Entities;
using VestTour.Domain.Enums;
using VestTour.Repository.Interface;
using VestTour.Repository.Models;
using VestTour.Repository.Repositories;
using VestTour.Repository.ValidationHelper;
using VestTour.Service.Interface;

namespace VestTour.Service.Implementation
{
    public class FabricService : IFabricService
    {
        private readonly IFabricRepository _fabricRepo;

        public FabricService(IFabricRepository fabricRepo)
        {
            _fabricRepo = fabricRepo;
        }

        public async Task<ServiceResponse<List<FabricModel>>> GetAllFabricsAsync()
        {
            var response = new ServiceResponse<List<FabricModel>>();
            try
            {
                var fabrics = await _fabricRepo.GetAllFabricsAsync();
                response.Data = fabrics;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<FabricModel>> GetFabricByIdAsync(int fabricId)
        {
            var response = new ServiceResponse<FabricModel>();
            try
            {
                var fabric = await _fabricRepo.GetFabricByIdAsync(fabricId);
                if (fabric == null)
                {
                    response.Success = false;
                    response.Message = "Fabric not found.";
                }
                else
                {
                    response.Data = fabric;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<int>> AddFabricAsync(FabricModel model)
        {
            var response = new ServiceResponse<int>();
            if (!ItemStatusValidate.IsValidStatus(model.Status))
            {
                response.Success = false;
                response.Message = "Invalid style status. Status must be Available or Unavailable";
                return response;
            }
            try
            {
                var fabricId = await _fabricRepo.AddFabricAsync(model);
                response.Data = fabricId;
                response.Success = true;
                response.Message = "Fabric added successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse> UpdateFabricAsync(int id, FabricModel model)
        {
            var response = new ServiceResponse();
            if (!ItemStatusValidate.IsValidStatus(model.Status))
            {
                response.Success = false;
                response.Message = "Invalid style status. Status must be Available or Unavailable";
                return response;
            }
            try
            {
                await _fabricRepo.UpdateFabricAsync(id, model);
                response.Success = true;
                response.Message = "Fabric updated successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse> DeleteFabricAsync(int fabricId)
        {
            var response = new ServiceResponse();
            try
            {
                await _fabricRepo.DeleteFabricAsync(fabricId);
                response.Success = true;
                response.Message = "Fabric deleted successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<List<FabricModel>>> GetFabricByTagAsync(FabricEnums? tag)
        {
            var response = new ServiceResponse<List<FabricModel>>();
            try
            {
                var fabrics = await _fabricRepo.GetFabricByTagAsync(tag);
                response.Data = fabrics;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }
        public async Task<ServiceResponse> UpdateStatusAsync(int itemId, string newStatus)
        {
            var response = new ServiceResponse();
            if (!ItemStatusValidate.IsValidStatus(newStatus))
            {
                response.Success = false;
                response.Message = "Invalid style status. Status must be Available or Unavailable";
                return response;
            }
            await _fabricRepo.UpdateStatusAsync(itemId, newStatus);

            // Return success response
            response.Success = true;
            response.Message = "Style status updated successfully.";

            return response;
        }
    }
}
