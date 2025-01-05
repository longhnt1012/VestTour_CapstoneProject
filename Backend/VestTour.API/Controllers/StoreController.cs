using Microsoft.AspNetCore.Mvc;
using VestTour.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestTour.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using VestTour.API.FileHandle;
using VestTour.Service.Services;
using VestTour.Service.Implementation;

namespace VestTour.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        // GET: api/Store
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreModel>>> GetStores()
        {
            var stores = await _storeService.GetAllStoresAsync();
            return Ok(stores);
        }

        // GET: api/Store/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreModel>> GetStore(int id)
        {
            var store = await _storeService.GetStoreByIdAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            return Ok(store);
        }

        // POST: api/Store
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<int>> CreateStore(StoreModel storeModel)
        {
            var newStoreId = await _storeService.CreateStoreAsync(storeModel);
            return CreatedAtAction(nameof(GetStore), new { id = newStoreId }, newStoreId);
        }

        // PUT: api/Store/5
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,store manager")]
        public async Task<IActionResult> UpdateStore(int id, StoreModel storeModel)
        {
            if (id != storeModel.StoreId)
            {
                return BadRequest("Store ID mismatch.");
            }
            await _storeService.UpdateStoreAsync(id, storeModel);
            return NoContent();
        }

        // DELETE: api/Store/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            await _storeService.DeleteStoreAsync(id);
            return NoContent();
        }
        [HttpGet("{storeId}/staff")]
        public async Task<IActionResult> GetStaffByStoreId(int storeId)
        {
            var staff = await _storeService.GetStaffByStoreIdAsync(storeId);
            return Ok(staff);
        }
        [HttpGet("userId/{userId}")]
        public async Task<IActionResult> GetStoreByUserId(int userId)
        {
            var store = await _storeService.GetStoreByUserId(userId);
            return Ok(store);
        }
        [HttpGet("manager-userId/{userId}")]
        public async Task<IActionResult> GetTailorPartnerByManagerID(int userId)
        {
            var store = await _storeService.GetTailorPartnerByManagerID(userId);
            return Ok(store);
        }
        [HttpPost("{storeId}/add-staff/{staffId}")]
        public async Task<IActionResult> AddStaffToStore(int storeId, int staffId)
        {
            var result = await _storeService.AddStaffToStoreAsync(storeId, staffId);

            if (result)
                return Ok(new { Message = "Staff added to store successfully." });

            return BadRequest(new { Message = "Failed to add staff to store. Staff not found or role not suitable." });
        }
        [HttpDelete("{storeId}/remove-staff/{staffId}")]
        public async Task<IActionResult> RemoveStaffFromStore(int storeId, int staffId)
        {
            var response = await _storeService.RemoveStaffFromStoreAsync(storeId, staffId);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }
        [HttpGet("GetStoreByStaff/{staffId}")]
        public async Task<IActionResult> GetStoreByStaffIdAsync(int staffId)
        {
            if (staffId <= 0)
            {
                return BadRequest("Invalid staff ID.");
            }

            var store = await _storeService.GetStoreByStaffIdAsync(staffId);

            if (store == null)
            {
                return NotFound("No store found for the given staff ID.");
            }

            return Ok(store);
        }
        [HttpGet("{storeId}/timeslots")]
        public async Task<IActionResult> GetStoreTimeSlots(int storeId)
        {
            var timeSlots = await _storeService.GetStoreTimeSlotsAsync(storeId);

            if (timeSlots == null || !timeSlots.Any())
                return NotFound(new { Message = "No available time slots for this store." });

            return Ok(timeSlots);
        }
        
        [HttpPost("{storeId}/image/upload")]
        public async Task<IActionResult> UploadImage(int storeId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "No file uploaded or file is empty." });
            }

            try
            {
                // Handle file upload
                var uploadHandler = new UploadHandle();
                string result = uploadHandler.Upload(file);

                // Check if upload was successful
                if (result.StartsWith("Extension is not valid") || result.StartsWith("Maximum size can be"))
                {
                    return BadRequest(new { message = result });
                }

                // Update avatar URL in the database
                string imageUrl = $"/Uploads/{result}"; // Assuming uploads are served from this path
                await _storeService.UpdateStoreImageAsync(storeId, imageUrl);

                return Ok(new { message = "Store Image updated successfully", imageUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPut("{id}/update-status")]
        public async Task<IActionResult> UpdateProductStatus(int id, [FromBody] string status)
        {
            var result = await _storeService.UpdateStatusAsync(id, status);

            if (!result.Success)
            {
                return BadRequest(new { Message = result.Message });
            }

            return Ok(new { Message = result.Message });
        }
    }
}
