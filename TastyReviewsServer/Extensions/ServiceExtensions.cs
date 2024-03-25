using Application;
using Application.Restaurant;
using Contracts;

namespace TastyReviewsServer.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IRestaurantService, RestaurantService>();
        }
    }
}
