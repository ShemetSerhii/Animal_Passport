using AnimalPassport.DataAccess.Blob.Interfaces;

namespace AnimalPassport.DataAccess.Blob.Managers
{
    internal class AttachmentBlobManager : BlobManager, IAttachmentBlobManager
    {
        public AttachmentBlobManager(IBlobContext blobStorage)
            : base(blobStorage.AttachmentContainer)
        {
        }
    }
}