using Microsoft.AspNetCore.Mvc;

namespace HelloBarWebhookApi.Controllers
{
    [ApiController]
    [Route("")]
    public class HelloBarController : ControllerBase
    {
        private readonly IHelloBarService _helloBarService;
        private readonly ILogger<HelloBarController> _logger;

        public HelloBarController(
            IHelloBarService helloBarService,
            ILogger<HelloBarController> logger)
        {
            _helloBarService = helloBarService;
            _logger = logger;
        }

        [HttpGet("webhook_hellobar_gamification")]
        public IActionResult WebhookHelloBar([FromQuery] string name, [FromQuery] string email, [FromQuery] string? winning_offer = null, [FromQuery] string? winning_offer_code = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return BadRequest("Email is required.");
                }

                int result = _helloBarService.InsertHelloBarEmail(
                    name ?? string.Empty,
                    email,
                    winning_offer,
                    winning_offer_code);

                return Ok(new
                {
                    success = true,
                    result = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Hello Bar webhook failed.");

                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while processing the webhook."
                });
            }
        }

        [HttpGet("webhook_hellobar")]
        public IActionResult WebhookHelloBar([FromQuery] string name, [FromQuery] string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    return BadRequest("Email is required.");
                }

                int result = _helloBarService.InsertHelloBarEmail(
                    name ?? string.Empty,
                    email);

                return Ok(new
                {
                    success = true,
                    result = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Hello Bar webhook failed.");

                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while processing the webhook."
                });
            }
        }
    }
}
