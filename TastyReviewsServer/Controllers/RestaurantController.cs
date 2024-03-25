using Application.Restaurant;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TastyReviewsServer.Model;
using Contracts;
using Infrastructure.Dto;
using Infrastructure.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace TastyReviewsServer.Controllers
{
    //[Authorize(Policy = "Owner")]
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController(IRestaurantService restaurantService,IMapper? mapper = null) : ControllerBase
    {
        private IRestaurantService _restaurantService = restaurantService;
        private IMapper _mapper = mapper;

        //[HttpGet]
        //public Task<IActionResult> GetAllRestaurant()
        //{
        //    var response = _restaurantService.GetAllRestaurant();
        //    var restaurants = _mapper.Map<IEnumerable<RestaurantModel>, IEnumerable<OwnerPostingsModel>>(response);
        //    return Task.FromResult<IActionResult>(Ok(restaurants));
        //}

        //[Authorize(Policy = "Owner")]
        [HttpPost]
        public Task<IActionResult> CreatePostings([FromBody] OwnerPostingsModel model)
        {
            //model.Images.Image = new Byte[1];
            //foreach (var modelss in model.Images)
            //{
            //    modelss.Image = new Byte[40];
            //}
            var postings = _mapper.Map<RestaurantModel>(model);
             _restaurantService.CreatePostings(postings);
            
            return Task.FromResult<IActionResult>(StatusCode(StatusCodes.Status200OK));
        }
    }
}
