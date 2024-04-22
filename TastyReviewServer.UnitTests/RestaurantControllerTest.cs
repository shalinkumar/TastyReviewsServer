using Application;
using Application.Restaurant;
using AutoMapper;
using Contracts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TastyReviewsServer.Controllers;
using TastyReviewsServer.Model;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Http;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace TastyReviewServer.UnitTests
{
    public class RestaurantControllerTest : BaseSetup
    {
        private IRestaurantService _restaurantService;
        private IRepositoryBase<RestaurantModel> _repositoryBase;
        private IRepositoryBase<RestaurantImages> _repositoryRestaurantImages;
        public RestaurantControllerTest()
        {
            //_restaurantService = new Mock<IRestaurantService>().Object;
            _repositoryBase = new RepositoryBase<RestaurantModel>(_dbContext);
            _repositoryRestaurantImages = new RepositoryBase<RestaurantImages>(_dbContext);
            _restaurantService = new RestaurantService(_repositoryBase,_mapper);            
        }

        [Fact]
        public void CreatePostings_Should_Insert()
        {
            //Arrange            
            var guid = Guid.NewGuid();
         
            string basePath = Environment.CurrentDirectory;

            string relativePath = "../../../Assets/Images/SpiceIndia.JPG";
            string relativeKashmirPath = "../../../Assets/Images/Kashmir.jpg";
        
            string spiceIndiaPath = Path.GetFullPath(relativePath,basePath);
            string kashmirPath = Path.GetFullPath(relativeKashmirPath, basePath);

            // Arrange.                    
           
            using (var stream = File.OpenRead(spiceIndiaPath))
            {
                var formFiles = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(spiceIndiaPath))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/JPG"
                };
                var model = new RestaurantPostingsModel()
                {
                    Guid = guid,
                    RestaurantName = "SpiceIndia",
                    Latitude = "234",
                    Longitude = "234",
                    Country = "Ireland",
                    PhoneNumber = "0851523834",
                    Address = "Test",
                    City = "Galway",
                    Region = "",
                    PostalCode = "H91D628",
                    CountryCode = "",
                    //CreationDate = DateTime.Now,
                    CreatedBy = "Test",
                    LastUpdatedBy = "Test",
                    //InteriorImage = ["", ""],
                    //ExteriorImage = ["", ""]
                    //Images = [new RestaurantImagesModel()
                    //{
                    //    Guid = guid,
                    //    IsInterior = true,
                    //    Inter = formFiles
                    //}
               //new RestaurantImagesModel()
               //{
               //    Guid = guid,
               //    IsInterior = true,
               //    Image = inputFile
               //}
               //]
                };

                //Act
                var response = new RestaurantController(_restaurantService, _mapper).CreatePostings(model);
                stream.Dispose();
                //Assert
                //Assert.True(((ObjectResult)response.Result).StatusCode == 200, "Success");              
            }
            
          
           
            
           
            //Assert.True(((ObjectResult)response.Result).StatusCode == 200, "Record Created");
        }

        public Byte[] ConvertImagetoByteArray(string path)
        {            
            MemoryStream memoryStream = new MemoryStream();
            if (File.Exists(path))
            {
                Image image = Image.FromFile(path);
                image.Save(memoryStream, ImageFormat.Png);
            }

            return memoryStream.ToArray();           
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
