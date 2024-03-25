using Application;
using Application.Restaurant;
using AutoMapper;
using Contracts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyReviewsServer.Controllers;
using TastyReviewsServer.Model;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;

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

            string relativePath = "../../../Assets/Images/SpiceIndia.jpg";
            string relativeKashmirPath = "../../../Assets/Images/Kashmir.jpg";
        
            string spiceIndiaPath = Path.GetFullPath(relativePath,basePath);
            string kashmirPath = Path.GetFullPath(relativeKashmirPath, basePath);

            var model = new OwnerPostingsModel()
            {
                Guid = guid,
                RestaurantName = "Test",
                Latitude = "234",
                Longitude = "234",
                Country = "Ireland",               
                PhoneNumber = "0851523834",
                Address = "Test",
                City = "Galway",
                Region = "",
                PostalCode = "H91D628",
                CountryCode = "",
                CreationDate = DateTime.Now,
                CreatedBy = "Test",
                LastUpdatedBy = "Test",
                Images = [new RestaurantImagesModel()
                {
                    Guid = guid,
                    IsInterior = true,
                    Image = ConvertImagetoByteArray(spiceIndiaPath)
                },
                    new RestaurantImagesModel()
                    {
                        Guid = guid,
                        IsInterior = true,
                        Image = ConvertImagetoByteArray(kashmirPath)
                    }]
            };
            //Act
            var response = new RestaurantController(_restaurantService, _mapper).CreatePostings(model);
            //Assert
            Assert.True(((ObjectResult)response.Result).StatusCode == 200, "Record Created");
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
