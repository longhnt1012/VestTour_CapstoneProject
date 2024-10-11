using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VestTour.Models;
using VestTour.Services;

namespace VestTour.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> AddNewLining (LiningModel model)
        {
            var newLiningID= await _liningRepo.AddLiningAsync(model);
            var lining = await _liningRepo.GetLiningByIdAsync (newLiningID);
            return lining ==null ? NotFound() : Ok(lining);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLining(int id, LiningModel model)
        {
            try
            {
                if(id != model.LiningId)
                {
                    return NotFound();
                }
                if(id != null)
                {
                    await _liningRepo.UpdateLiningAsync(id, model);
                    return Ok();
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
        public async Task<IActionResult> DeleteLining(int id)
        {
            try
            {
                if(id != null)
                {
                    await _liningRepo.DeleteLiningAsync(id);
                    return Ok();
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
