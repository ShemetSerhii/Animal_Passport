using Microsoft.Azure.Storage.Blob;

namespace AnimalPassport.DataAccess.Blob.Interfaces
{
    public interface IBlobContext
    {
        CloudBlobContainer AttachmentContainer { get; }

        CloudBlobContainer PictureContainer { get; }
    }
}