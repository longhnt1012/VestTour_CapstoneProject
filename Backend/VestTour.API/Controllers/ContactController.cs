using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Helpers;
using VestTour.Repository.Models;
using VestTour.Service.Interfaces;

namespace VestTour.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IEmailHelper _emailHelper;

        public ContactController(IEmailHelper emailHelper)
        {
            _emailHelper = emailHelper;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendContactEmail([FromBody] ContactRequest contactRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Call the CustomerSendContactEmail method to send the email
                await _emailHelper.CustomerSendContactEmail(contactRequest);
                return Ok(new { Message = "Your message has been sent successfully." });
            }
            catch (Exception ex)
            {
                // Handle exceptions (consider logging it as well)
                return StatusCode(500, new { Message = "An error occurred while sending your message.", Error = ex.Message });
            }
        }
    }

}
