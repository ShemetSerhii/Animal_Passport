using AnimalPassport.DataAccess.Blob.Interfaces;

namespace AnimalPassport.DataAccess.Blob.Managers
{
    internal class PictureBlobManager : BlobManager, IPictureBlobManager
    {
        public PictureBlobManager(IBlobContext blobStorage)
            : base(blobStorage.PictureContainer)
        {
        }
    }
}