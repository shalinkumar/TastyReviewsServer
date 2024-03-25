using Infrastructure.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using TastyReviewsServer.Automapper;
using AutoMapper;
using TastyReviewsServer.Extensions;

namespace TastyReviewsServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHealthChecks();

            services.AddCors(options =>
            {
                options.AddPolicy("CORSPolicy",
                    builder =>
                    {
                        builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins("http://localhost:3000", "http://192.168.0.212:8081");
                    });
            });
            services.AddSwaggerGen(swaggerGenOptions =>
            {
                swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "Tasty Review", Version = "v1" });
            });

            // For Entity Framework
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));          

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = true,
                    ValidAudience = _configuration["JWT:ValidAudience"],
                    ValidIssuer = _configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]))
                };
            });

            //services.AddAutoMapper(typeof(AutoMapperProfileConfiguration));
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfileConfiguration());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.ConfigureServices();
            services.ConfigureRepository();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(swaggerUIOptions =>
            {
                swaggerUIOptions.DocumentTitle = "Tasty Reviews";
                swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Api.");
                swaggerUIOptions.RoutePrefix = string.Empty;
            });


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("CORSPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
