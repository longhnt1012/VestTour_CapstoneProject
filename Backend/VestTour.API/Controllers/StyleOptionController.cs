using Microsoft.AspNetCore.Mvc;
using VestTour.Service.Interfaces;
using VestTour.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult<int>> AddStyleOption(StyleOptionModel styleOptionModel)
        {
            var newStyleOptionId = await _styleOptionService.AddStyleOptionAsync(styleOptionModel);
            return CreatedAtAction(nameof(GetStyleOptionById), new { id = newStyleOptionId }, newStyleOptionId);
        }

        [HttpPut("{id}")]
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
        public async Task<IActionResult> DeleteStyleOption(int id)
        {
            await _styleOptionService.DeleteStyleOptionAsync(id);
            return NoContent();
        }
    }
}
