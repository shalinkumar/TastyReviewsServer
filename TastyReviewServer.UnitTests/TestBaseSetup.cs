using AutoMapper;
using Infrastructure;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using TastyReviewsServer.Automapper;
using TastyReviewsServer.Controllers;

namespace TastyReviewServer.UnitTests
{
    public class TestBaseSetup : BaseSetup
    {
        //private ApplicationDbContext DbContext;
        protected UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IConfiguration _configuration;
        //private IMapper _mapper;

        public TestBaseSetup()
        {

          //  var objBuilder = new ConfigurationBuilder()
          //.SetBasePath(Directory.GetCurrentDirectory())
          //.AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
          //  IConfiguration conManager = objBuilder.Build();

            //DbContextOptionsBuilder<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(conManager.GetConnectionString("DefaultConnection"));

            //_dbContext = new ApplicationDbContext(options.Options);

            var userStore = new UserStore<ApplicationUser>(_dbContext);
            _userManager = new UserManager<ApplicationUser>(userStore, null,
            null,
            null,
            null, null, null, null, null);
            var store = new RoleStore<IdentityRole>(_dbContext);
            _roleManager = new RoleManager<IdentityRole>(store, null, null, null, null);
            //_configuration = conManager;

        //    MapperConfiguration mapperConfig = new MapperConfiguration(
        //cfg =>
        //{
        //    cfg.AddProfile(new AutoMapperProfileConfiguration());
        //});
        //    IMapper mapper = new Mapper(mapperConfig);
        //    _mapper = mapper;
        }

        public Task<IActionResult> RegisterUser(string userName, string email, string password)
        {
            return new AuthenticateController(_userManager, _roleManager, _configuration, _mapper).Register(new RegisterModel
            {
                UserName = userName,
                Email = email,
                Password = password
            });
        }

        public Task<IActionResult> InsertAdminUser(string userName, string email, string password)
        {
            return new AuthenticateController(_userManager, _roleManager, _configuration).RegisterAdmin(new RegisterModel
            {
                UserName = userName,
                Email = email,
                Password = password
            });
        }

        public Task<IActionResult> Login(string userName, string password)
        {
            return new AuthenticateController(_userManager, _roleManager, _configuration).Login(new LoginModel
            {
                UserName = userName,
                Password = password
            });
        }
    }
}
