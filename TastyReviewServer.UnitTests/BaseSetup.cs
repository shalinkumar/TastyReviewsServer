using AutoMapper;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyReviewsServer.Automapper;

namespace TastyReviewServer.UnitTests
{
    public abstract class BaseSetup
    {
        internal IMapper _mapper;
        internal readonly IConfiguration _configuration;
        internal ApplicationDbContext _dbContext;
        protected BaseSetup() 
        {

            var objBuilder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
            IConfiguration conManager = objBuilder.Build();
            _configuration = conManager;

            DbContextOptionsBuilder<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(conManager.GetConnectionString("DefaultConnection"));

            _dbContext = new ApplicationDbContext(options.Options);

            MapperConfiguration mapperConfig = new MapperConfiguration(
            cfg =>
            {
            cfg.AddProfile(new AutoMapperProfileConfiguration());
            });
            IMapper mapper = new Mapper(mapperConfig);
            _mapper = mapper;
        }
    }
}
