using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Repository.Interface;
using VestTour.Repository.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace VestTour.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class LiningsController : ControllerBase
    {
        private readonly ILiningRepository _liningRepo;

        public LiningsController(ILiningRepository repo)
        {
            _liningRepo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLining()
        {
            try
            {
                return Ok(await _liningRepo.GetAllLiningAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLiningByID(int id)
        {
            try
            {
                var lining = await _liningRepo.GetLiningByIdAsync(id);
                return lining == null ? NotFound() : Ok(lining);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddNewLining(LiningModel model)
        {
            var newLiningID = await _liningRepo.AddLiningAsync(model);
            var lining = await _liningRepo.GetLiningByIdAsync(newLiningID);
            return lining == null ? NotFound() : Ok(lining);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin,store manager")]
        public async Task<IActionResult> UpdateLining(int id, LiningModel model)
        {
            try
            {
                if (id != model.LiningId)
                {
                    return NotFound();
                }
                if (id != null)
                {
                    await _liningRepo.UpdateLiningAsync(id, model);
                    return Ok(Success.SuccessUpdateLining);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteLining(int id)
        {
            try
            {
                if (id != null)
                {
                    await _liningRepo.DeleteLiningAsync(id);
                    return Ok(Success.SuccessDeleteLining);
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
