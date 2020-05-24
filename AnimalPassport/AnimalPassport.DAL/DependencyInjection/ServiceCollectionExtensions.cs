using AnimalPassport.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalPassport.DataAccess.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddDbContext<AnimalPassportDbContext>(options =>
                options.UseSqlServer("Server=.;Database=PetPassport;Trusted_Connection=True;"));
            services.AddScoped<DbContext, AnimalPassportDbContext>(provider => provider.GetService<AnimalPassportDbContext>());
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
    }
}