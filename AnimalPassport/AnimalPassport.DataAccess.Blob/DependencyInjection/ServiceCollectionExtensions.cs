using AnimalPassport.DataAccess.Blob.Interfaces;
using AnimalPassport.DataAccess.Blob.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalPassport.DataAccess.Blob.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBlobManagers(this IServiceCollection services)
        {
            services.AddScoped<IBlobContext>(provider => new BlobContext());
            services.AddScoped<IAttachmentBlobManager, AttachmentBlobManager>();
            services.AddScoped<IPictureBlobManager, PictureBlobManager>();
        }
    }
}