using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TastyReviewsServer.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsRatingsController : ControllerBase
    {
        public ReviewsRatingsController() { }

        [HttpGet]
        [Route("register")]
        public string Get()
        {
            return "Shalin";
        }
    }
}
