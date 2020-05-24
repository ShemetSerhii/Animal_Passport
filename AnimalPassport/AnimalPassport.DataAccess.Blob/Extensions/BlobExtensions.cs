using AnimalPassport.DataAccess.Blob.Models;
using Microsoft.Azure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;

namespace AnimalPassport.DataAccess.Blob.Extensions
{
    public static class BlobExtensions
    {
        public static async Task<FileModel> AsFileModelAsync(this CloudBlockBlob blob)
        {
            await using var blobStream = new MemoryStream();

            await blob.FetchAttributesAsync();
            await blob.DownloadToStreamAsync(blobStream);

            return new FileModel
            {
                Content = blobStream.ToArray(),
                ContentType = blob.Properties.ContentType,
                FilePath = blob.Name
            };
        }
    }
}