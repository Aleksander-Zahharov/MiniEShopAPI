/**
 * BaseController provides common functionality for all API controllers.
 */
using Microsoft.AspNetCore.Mvc;

namespace MiniEShopAPI.Controllers
{
    [ApiController] // Marks this class as an API controller
    [Route("api/[controller]")] // Defines the base route for all endpoints in this controller
    public abstract class BaseController : ControllerBase
    {
        // Base controller for shared functionality across all controllers

        [HttpGet("ping")] // Defines a GET endpoint at /api/[controller]/ping
        public IActionResult Ping()
        {
            return Ok("API is working!"); // Returns a simple success message
        }
    }
}