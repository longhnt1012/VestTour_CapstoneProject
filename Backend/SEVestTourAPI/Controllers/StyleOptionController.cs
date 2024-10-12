using Microsoft.AspNetCore.Mvc;
using SEVestTourAPI.Models;
using SEVestTourAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StyleOptionController : ControllerBase
    {
        private readonly IStyleOptionRepository _styleOptionRepository;

        public StyleOptionController(IStyleOptionRepository styleOptionRepository)
        {
            _styleOptionRepository = styleOptionRepository;
        }

        // GET: api/styleoption
        [HttpGet]
        public async Task<ActionResult<List<StyleOptionModel>>> GetAllStyleOptions()
        {
            var styleOptions = await _styleOptionRepository.GetAllStyleOptionsAsync();
            return Ok(styleOptions);
        }

        // GET: api/styleoption/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<StyleOptionModel>> GetStyleOptionById(int id)
        {
            var styleOption = await _styleOptionRepository.GetStyleOptionByIdAsync(id);
            if (styleOption == null)
            {
                return NotFound();
            }
            return Ok(styleOption);
        }

        // POST: api/styleoption
        [HttpPost]
        public async Task<ActionResult<int>> AddStyleOption(StyleOptionModel styleOptionModel)
        {
            var newStyleOptionId = await _styleOptionRepository.AddStyleOptionAsync(styleOptionModel);
            return CreatedAtAction(nameof(GetStyleOptionById), new { id = newStyleOptionId }, newStyleOptionId);
        }

        // PUT: api/styleoption/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStyleOption(int id, StyleOptionModel styleOptionModel)
        {
            if (id != styleOptionModel.StyleOptionId)
            {
                return BadRequest();
            }

            await _styleOptionRepository.UpdateStyleOptionAsync(id, styleOptionModel);
            return NoContent();
        }

        // DELETE: api/styleoption/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStyleOption(int id)
        {
            await _styleOptionRepository.DeleteStyleOptionAsync(id);
            return NoContent();
        }
    }
}
