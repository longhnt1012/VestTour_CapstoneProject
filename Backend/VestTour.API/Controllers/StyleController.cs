using Microsoft.AspNetCore.Mvc;
using VestTour.Service.Interfaces;
using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using VestTour.Service.Interface;

namespace VestTour.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class StyleController : ControllerBase
    {
        private readonly IStyleService _styleService;

        public StyleController(IStyleService styleService)
        {
            _styleService = styleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<StyleModel>>> GetAllStyles()
        {
            var styles = await _styleService.GetAllStylesAsync();
            return Ok(styles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StyleModel>> GetStyleById(int id)
        {
            var style = await _styleService.GetStyleByIdAsync(id);
            if (style == null)
            {
                return NotFound();
            }
            return Ok(style);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<int>> AddStyle(StyleModel styleModel)
        {
            var newStyleId = await _styleService.AddStyleAsync(styleModel);
            return CreatedAtAction(nameof(GetStyleById), new { id = newStyleId }, newStyleId);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin,store manager")]
        public async Task<IActionResult> UpdateStyle(int id, StyleModel styleModel)
        {
            if (id != styleModel.StyleId)
            {
                return BadRequest();
            }

            await _styleService.UpdateStyleAsync(id, styleModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteStyle(int id)
        {
            await _styleService.DeleteStyleAsync(id);
            return NoContent();
        }
        [HttpPatch("{id}/status")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ServiceResponse>> UpdateStatusAsync(int id, [FromBody] string newStatus)
        {
            var response = await _styleService.UpdateStatusAsync(id, newStatus);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
