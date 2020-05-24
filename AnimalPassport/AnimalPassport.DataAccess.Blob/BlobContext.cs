using System;
using System.Threading.Tasks;
using AnimalPassport.DataAccess.Blob.Interfaces;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace AnimalPassport.DataAccess.Blob
{
    public class BlobContext : IBlobContext
    {
        private readonly Lazy<CloudBlobContainer> _attachmentContainer;
        private readonly Lazy<CloudBlobContainer> _pictureContainer;

        public BlobContext()
        {
            _attachmentContainer = new Lazy<CloudBlobContainer>(() => GetOrCreateContainer("attachment"));
            _pictureContainer = new Lazy<CloudBlobContainer>(() => GetOrCreateContainer("picture"));
        }

        public CloudBlobContainer AttachmentContainer => _attachmentContainer.Value;

        public CloudBlobContainer PictureContainer => _pictureContainer.Value;

        private CloudBlobContainer GetOrCreateContainer(string containerName)
        {
            var storageAccount = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);

            CreateContainerIfNotExistsAsync(container).ConfigureAwait(false);

            return container;
        }

        private async Task CreateContainerIfNotExistsAsync(CloudBlobContainer container)
        {
            if (await container.CreateIfNotExistsAsync())
            {
                await container.SetPermissionsAsync(
                    new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob,
                    });
            }
        }
    }
}