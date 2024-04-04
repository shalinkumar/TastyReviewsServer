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
        public Task<IActionResult> CreatePostings([FromBody] RestaurantPostingsModel model)
        {
            //var files = Request.Form.Files;


            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                foreach (var img in model.Images)
                {
                    img.FormImage.CopyTo(ms);

                    fileBytes = ms.ToArray();
                    img.Image = fileBytes;
                }                              
            }
            var postings = _mapper.Map<RestaurantModel>(model);
            _restaurantService.CreatePostings(postings);


            //var path = Path.Combine(@"D:\path", "test.JPG");
            //using (var stream = new FileStream(path, FileMode.Create))
            //{
            //    MemoryStream memoryStream = new MemoryStream(fileBytes);

            //    Image returnImage = Image.FromStream(memoryStream);

            //    returnImage.Save(stream,ImageFormat.Jpeg);
            //    stream.Dispose();
            //}
            //foreach (var modelss in model.Images)
            //{
            //    modelss.Image = new Byte[40];
            //}


            return Task.FromResult<IActionResult>(StatusCode(StatusCodes.Status200OK));
        }
    }
}
