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
            CreateMap<OwnerPostingsModel, RestaurantModel>();
            CreateMap<RestaurantModel, OwnerPostingsModel>();
            CreateMap<RestaurantImages, RestaurantImagesModel>();
            CreateMap<RestaurantImagesModel, RestaurantImages>();
        }
    }
}
