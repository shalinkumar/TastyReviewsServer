using AutoMapper;
using Infrastructure.Entities;
using Infrastructure.Dto;
using TastyReviewsServer.Model;

namespace TastyReviewsServer.Automapper
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration() {
            CreateMap<RegisterModel, ApplicationUser>();
            CreateMap<RestaurantPostingsModel, RestaurantModel>();
            CreateMap<RestaurantModel, RestaurantPostingsModel>();
            CreateMap<RestaurantImages, RestaurantImagesModel>();
            CreateMap<RestaurantImagesModel, RestaurantImages>();
        }
    }
}
