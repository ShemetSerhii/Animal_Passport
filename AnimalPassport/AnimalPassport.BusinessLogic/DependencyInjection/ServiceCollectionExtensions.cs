using AnimalPassport.BusinessLogic.Interfaces;
using AnimalPassport.BusinessLogic.Managers;
using AnimalPassport.DataAccess.Blob.DependencyInjection;
using AnimalPassport.DataAccess.DependencyInjection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalPassport.BusinessLogic.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBusinessLogicComponents(this IServiceCollection services)
        {
            services.AddUnitOfWork();
            services.AddBlobManagers();

            services.AddAutoMapper(typeof(ServiceCollectionExtensions));

            services.AddManagers();
        }

        private static void AddManagers(this IServiceCollection services)
        {
            services.AddScoped<IAnimalManager, AnimalManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IAttachmentManager, AttachmentManager>();
            services.AddScoped<IMedicalCardManager, MedicalCardManager>();
        }
    }
}