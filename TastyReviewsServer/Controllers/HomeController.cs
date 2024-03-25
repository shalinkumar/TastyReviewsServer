using Application.Restaurant;
using AutoMapper;
using Contracts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using TastyReviewsServer.Model;

namespace TastyReviewsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController(IRestaurantService restaurantService, IMapper? mapper = null) : ControllerBase
    {
        private IRestaurantService _restaurantService = restaurantService;
        private IMapper _mapper = mapper;

        [HttpGet]
        public Task<IActionResult> GetAllRestaurant()
        {
            var response = _restaurantService.GetAllRestaurant();
            var restaurants = _mapper.Map<IEnumerable<RestaurantModel>, IEnumerable<OwnerPostingsModel>>(response);
            return Task.FromResult<IActionResult>(Ok(restaurants));
        }
    }
}
