using Infrastructure.Dto;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRestaurantService
    {
        void CreatePostings(RestaurantModel model);
        Task<RestaurantModel> GetPostingsById(int id);
        IEnumerable<RestaurantModel> GetAllRestaurant();
    }
}
