using Microsoft.AspNetCore.Mvc;
using VestTour.Service.Interfaces;
using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using VestTour.Service.Implementation;
using VestTour.Service.Interface;

namespace VestTour.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class StyleOptionController : ControllerBase
    {
        private readonly IStyleOptionService _styleOptionService;

        public StyleOptionController(IStyleOptionService styleOptionService)
        {
            _styleOptionService = styleOptionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<StyleOptionModel>>> GetAllStyleOptions()
        {
            var styleOptions = await _styleOptionService.GetAllStyleOptionsAsync();
            return Ok(styleOptions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StyleOptionModel>> GetStyleOptionById(int id)
        {
            var styleOption = await _styleOptionService.GetStyleOptionByIdAsync(id);
            if (styleOption == null)
            {
                return NotFound();
            }
            return Ok(styleOption);
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<int>> AddStyleOption(StyleOptionModel styleOptionModel)
        {
            var newStyleOptionId = await _styleOptionService.AddStyleOptionAsync(styleOptionModel);
            return CreatedAtAction(nameof(GetStyleOptionById), new { id = newStyleOptionId }, newStyleOptionId);
        }

        [HttpPut("{id}")]
       // [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateStyleOption(int id, StyleOptionModel styleOptionModel)
        {
            if (id != styleOptionModel.StyleOptionId)
            {
                return BadRequest();
            }

            await _styleOptionService.UpdateStyleOptionAsync(id, styleOptionModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteStyleOption(int id)
        {
            await _styleOptionService.DeleteStyleOptionAsync(id);
            return NoContent();
        }
        [HttpPatch("{id}/status")]
       // [Authorize(Roles = "admin")]
        public async Task<ActionResult<ServiceResponse>> UpdateStatusAsync(int id, [FromBody] string newStatus)
        {
            var response = await _styleOptionService.UpdateStatusAsync(id, newStatus);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
