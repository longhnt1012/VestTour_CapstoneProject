using Microsoft.AspNetCore.Mvc;
using SEVestTourAPI.Models;
using SEVestTourAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StyleController : ControllerBase
    {
        private readonly IStyleRepository _styleRepository;

        public StyleController(IStyleRepository styleRepository)
        {
            _styleRepository = styleRepository;
        }

        // GET: api/style
        [HttpGet]
        public async Task<ActionResult<List<StyleModel>>> GetAllStyles()
        {
            var styles = await _styleRepository.GetAllStylesAsync();
            return Ok(styles);
        }

        // GET: api/style/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<StyleModel>> GetStyleById(int id)
        {
            var style = await _styleRepository.GetStyleByIdAsync(id);
            if (style == null)
            {
                return NotFound();
            }
            return Ok(style);
        }

        // POST: api/style
        [HttpPost]
        public async Task<ActionResult<int>> AddStyle(StyleModel styleModel)
        {
            var newStyleId = await _styleRepository.AddStyleAsync(styleModel);
            return CreatedAtAction(nameof(GetStyleById), new { id = newStyleId }, newStyleId);
        }

        // PUT: api/style/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStyle(int id, StyleModel styleModel)
        {
            if (id != styleModel.StyleId)
            {
                return BadRequest();
            }

            await _styleRepository.UpdateStyleAsync(id, styleModel);
            return NoContent();
        }

        // DELETE: api/style/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStyle(int id)
        {
            await _styleRepository.DeleteStyleAsync(id);
            return NoContent();
        }
    }
}
