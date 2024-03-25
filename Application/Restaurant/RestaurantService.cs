using AutoMapper;
using Infrastructure;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Infrastructure.Dto;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Restaurant
{
    public class RestaurantService(IRepositoryBase<RestaurantModel> repositoryBase, IMapper mapper) : IRestaurantService
    {
        private readonly IRepositoryBase<RestaurantModel> _repositoryBase = repositoryBase;
        //private readonly IRepositoryBase<RestaurantImages> _repRestaurantImages = repRestaurantImages;
        private IMapper _mapper = mapper;

        public async Task<RestaurantModel> GetPostingsById(int id) => await _repositoryBase.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();

        public IEnumerable<RestaurantModel> GetAllRestaurant()
        {
            return _repositoryBase.FindAll().Include(x=>x.Images).ToList();
        }

        public void CreatePostings(RestaurantModel model)
        {            
            _repositoryBase.Create(model);            
            _repositoryBase.SaveChangesAsync();            
        }        
    }
}
