using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainMicroService.Controllers
{
    [Route("api/flight")]
    [AllowAnonymous]
    public class FlightController : Controller
    {
        #region Constructor

        public FlightController()
        {
            
        }

        #endregion

        #region Methods
        
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok("Service started");
        }

        #endregion
    }
}