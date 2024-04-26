using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TastyReviewsServer.Model;
using Contracts;
using System.Drawing;
using System.Drawing.Imaging;
using Infrastructure.Entities;

//using static System.Net.Mime.MediaTypeNames;

namespace TastyReviewsServer.Controllers
{
    [Authorize(Roles = UserRoles.Owner)]
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController(IRestaurantService restaurantService,IMapper? mapper = null) : ControllerBase
    {
        private IRestaurantService _restaurantService = restaurantService;
        private IMapper _mapper = mapper;
      
        
        [HttpPost]
        public Task<IActionResult> CreatePostings(RestaurantPostingsModel model)
        {
           
            using (var ms = new MemoryStream())
            {
                foreach (var url in model.InteriorImage)
                {                  
                    model.Images.Add(new RestaurantImagesModel
                    {
                        ImageUrl = url,
                        Guid = model.Guid,
                        IsInterior = true
                    });
                }


                foreach (var url in model.ExteriorImage)
                {                  
                    model.Images.Add(new RestaurantImagesModel
                    {
                        ImageUrl = url,
                        Guid = model.Guid,
                        IsInterior = false
                    });
                }
            }
            var postings = _mapper.Map<RestaurantModel>(model);
            _restaurantService.CreatePostings(postings);


            return Task.FromResult<IActionResult>(base.StatusCode(StatusCodes.Status200OK));
        }
    }
}
