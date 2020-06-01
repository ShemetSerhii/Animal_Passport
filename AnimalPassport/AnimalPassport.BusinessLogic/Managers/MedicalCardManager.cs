using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalPassport.BusinessLogic.DataTransferObjects;
using AnimalPassport.BusinessLogic.Interfaces;
using AnimalPassport.DataAccess.Blob.Interfaces;
using AnimalPassport.DataAccess.Interfaces;
using AnimalPassport.Entities.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AnimalPassport.BusinessLogic.Managers
{
    public class MedicalCardManager : IMedicalCardManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<MedicalOperation> _medicalOperationRepository;
        private readonly IRepository<Attachment> _attachmentRepository;
        private readonly IMapper _mapper;
        private readonly IAttachmentBlobManager _attachmentBlobManager;

        public MedicalCardManager(IUnitOfWork unitOfWork, IMapper mapper, IAttachmentBlobManager attachmentBlobManager)
        {
            _unitOfWork = unitOfWork;
            _medicalOperationRepository = unitOfWork.GetRepository<MedicalOperation>();
            _attachmentRepository = unitOfWork.GetRepository<Attachment>();
            _mapper = mapper;
            _attachmentBlobManager = attachmentBlobManager;
        }

        public async Task<IEnumerable<MedicalOperationDto>> GetAnimalMedRowsAsync(Guid animalId)
        {
            var medRows = await _medicalOperationRepository.GetAsync(m => m.AnimalId == animalId,
                includeProperties: source => source.Include(m => m.Attachments));

            return _mapper.Map<List<MedicalOperationDto>>(medRows).OrderByDescending(m => m.Date);
        }

        public async Task<Guid> AddMedicalCardRowAsync(Guid animalId, MedicalRowDto medicalRow)
        {
            var medicalOperation = _mapper.Map<MedicalOperation>(medicalRow);

            medicalOperation.Date = DateTime.UtcNow;
            medicalOperation.AnimalId = animalId;

            if (DateTime.TryParse(medicalRow.DateExpiry, out DateTime date))
            {
                medicalOperation.DateExpiry = date;
            }

            var id = _medicalOperationRepository.Create(medicalOperation);

            await _unitOfWork.SaveChangesAsync();

            return id;
        }

        public async Task UpdateMedicalCardRowAsync(Guid medicalId, MedicalRowDto medicalRow)
        {
            var medicalOperation = await _medicalOperationRepository.GetAsync(medicalId);

            _mapper.Map(medicalRow, medicalOperation);

            medicalOperation.DateExpiry = DateTime.TryParse(medicalRow.DateExpiry, out DateTime date) ? (DateTime?)date : null;

            _medicalOperationRepository.Update(medicalOperation);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteMedicalRowAsync(Guid medicalRowId)
        {
            var medicalRow = await _medicalOperationRepository.GetAsync(medicalRowId, source => source.Include(m => m.Attachments));

            foreach (var attachment in medicalRow.Attachments)
            {
                if (!string.IsNullOrEmpty(attachment.FilePath))
                {
                    await _attachmentBlobManager.DeleteFileAsync(attachment.FilePath);
                }

                _attachmentRepository.Delete(attachment);
            }

            _medicalOperationRepository.Delete(medicalRow);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}