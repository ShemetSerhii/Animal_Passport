using System;
using System.IO;
using System.Threading.Tasks;
using AnimalPassport.BusinessLogic.DataTransferObjects;
using AnimalPassport.BusinessLogic.Interfaces;
using AnimalPassport.DataAccess.Blob.Interfaces;
using AnimalPassport.DataAccess.Blob.Models;
using AnimalPassport.DataAccess.Interfaces;
using AnimalPassport.Entities.Entities;
using AutoMapper;

namespace AnimalPassport.BusinessLogic.Managers
{
    public class AttachmentManager : IAttachmentManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Attachment> _attachmentRepository;
        private readonly IAttachmentBlobManager _attachmentBlobManager;
        private readonly IMapper _mapper;

        public AttachmentManager(IUnitOfWork unitOfWork, IAttachmentBlobManager attachmentBlobManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _attachmentRepository = unitOfWork.GetRepository<Attachment>();
            _attachmentBlobManager = attachmentBlobManager;
            _mapper = mapper;
        }

        public async Task UploadAttachmentAsync(Guid medicalRowId, FileDto fileDto)
        {
            var path = Path.Combine($"{medicalRowId}", fileDto.FileName);
            var attachment = new Attachment
            {
                FileName = fileDto.FileName,
                MedicalOperationId = medicalRowId,
                CreationDate = DateTime.UtcNow,
                FilePath = path
            };
            var file = _mapper.Map<FileModel>(fileDto);
            file.FilePath = path;

            _attachmentRepository.Create(attachment);

            await _attachmentBlobManager.UploadFileAsync(file);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<FileDto> DownloadAttachmentAsync(Guid attachmentId)
        {
            var attachment = await _attachmentRepository.GetAsync(attachmentId);
            var file = await _attachmentBlobManager.DownloadFileAsync(attachment.FilePath);

            var fileDto = _mapper.Map<FileDto>(file);
            fileDto.FileName = attachment.FileName;

            return fileDto;
        }

        public async Task DeleteAttachmentAsync(Guid attachmentId)
        {
            var attachment = await _attachmentRepository.GetAsync(attachmentId);

            if (!string.IsNullOrEmpty(attachment.FilePath))
            {
                await _attachmentBlobManager.DeleteFileAsync(attachment.FilePath);
            }

            _attachmentRepository.Delete(attachment);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}