using System;
using System.Threading.Tasks;
using AnimalPassport.BusinessLogic.DataTransferObjects;

namespace AnimalPassport.BusinessLogic.Interfaces
{
    public interface IAttachmentManager
    {
        Task UploadAttachmentAsync(Guid medicalRowId, FileDto fileDto);

        Task<FileDto> DownloadAttachmentAsync(Guid attachmentId);

        Task DeleteAttachmentAsync(Guid attachmentId);
    }
}