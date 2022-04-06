using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DevTrustTest.BLL.ServiceInterfaces;
using DevTrustTest.BLL.Services;
using DevTrustTest.DAL;
using DevTrustTest.DAL.UnitOfWork;

namespace DevTrustTest.BLL.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTest(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddDbContext<MyTestContext>(options => options.UseSqlServer(configuration.GetConnectionString("MyTest")));
        }
    }
}
