using System;
using System.Net.Mime;
using System.Threading.Tasks;
using AnimalPassport.DataAccess.Blob.Extensions;
using AnimalPassport.DataAccess.Blob.Interfaces;
using AnimalPassport.DataAccess.Blob.Models;
using Microsoft.Azure.Storage.Blob;

namespace AnimalPassport.DataAccess.Blob.Managers
{
    internal abstract class BlobManager : IBlobManager
    {
        protected BlobManager(CloudBlobContainer container)
        {
            BlobContainer = container;
        }

        protected CloudBlobContainer BlobContainer { get; }

        public async Task<FileModel> DownloadFileAsync(string filePath)
        {
            var blob = BlobContainer.GetBlockBlobReference(filePath);

            return !await blob.ExistsAsync() ? null : await blob.AsFileModelAsync();
        }

        public async Task UploadFileAsync(FileModel file)
        {
            var blob = BlobContainer.GetBlockBlobReference(file.FilePath);
            blob.Properties.ContentType = file.ContentType ?? MediaTypeNames.Application.Octet;

            await EnsureBlobDoesNotExistAsync(blob);

            await blob.UploadFromByteArrayAsync(file.Content, 0, file.Content.Length);
        }

        public Task DeleteFileAsync(string filePath)
        {
            var blob = BlobContainer.GetBlockBlobReference(filePath);

            return blob.DeleteIfExistsAsync();
        }

        public Task<bool> ExistsAsync(string filePath)
        {
            var blob = BlobContainer.GetBlockBlobReference(filePath);

            return blob.ExistsAsync();
        }

        private static async Task EnsureBlobDoesNotExistAsync(CloudBlob blob)
        {
            if (await blob.ExistsAsync())
            {
                throw new ArgumentException($"Blob '{blob.Name}' is already exists", nameof(blob));
            }
        }
    }
}