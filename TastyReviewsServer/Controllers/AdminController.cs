using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TastyReviewsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy ="Admin")]
    public class AdminController : ControllerBase
    {

        [HttpGet]
        [Route("register")]
        public string Get()
        {
            return "Shalin";
        }
    }
}
