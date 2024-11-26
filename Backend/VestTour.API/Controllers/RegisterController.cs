using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Service.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using VestTour.Service.Interface;
using VestTour.Repository.Constants;
using Microsoft.AspNetCore.Cors;

namespace VestTourApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registerService;

        public RegisterController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _registerService.RegisterUserAsync(registerModel);

            if (result == Success.RegistrationSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmRequest confirmEmailRequest)
        {
            if (string.IsNullOrWhiteSpace(confirmEmailRequest.Email) || string.IsNullOrWhiteSpace(confirmEmailRequest.Otp))
            {
                return BadRequest("Email and OTP are required.");
            }

            var isOtpValid = await _registerService.ConfirmEmailAsync(confirmEmailRequest.Email, confirmEmailRequest.Otp);

            if (isOtpValid)
            {
                return Ok(new { Message = "Email confirmed successfully. Your account is now active." });
            }
            else
            {
                return BadRequest("Invalid OTP. Please try again.");
            }
        }
        [HttpGet]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            try
            {
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("resend-otp")]
        public async Task<IActionResult> ResendOtp([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(new { message = "Email is required." });
            }

            var result = await _registerService.ResendOtpAsync(email);

            if (result == Success.OtpResentSuccess)
            {
                return Ok(new { message = result });
            }

            return BadRequest(new { message = result });
        }
    }
}
