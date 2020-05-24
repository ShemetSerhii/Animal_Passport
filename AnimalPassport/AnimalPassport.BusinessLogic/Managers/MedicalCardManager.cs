using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalPassport.BusinessLogic.DataTransferObjects;
using AnimalPassport.BusinessLogic.Interfaces;
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
        private readonly IMapper _mapper;

        public MedicalCardManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _medicalOperationRepository = unitOfWork.GetRepository<MedicalOperation>();
            _mapper = mapper;
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

            var id = _medicalOperationRepository.Create(medicalOperation);

            await _unitOfWork.SaveChangesAsync();

            return id;
        }

        public async Task UpdateMedicalCardRowAsync(Guid medicalId, MedicalRowDto medicalRow)
        {
            var medicalOperation = await _medicalOperationRepository.GetAsync(medicalId);

            _mapper.Map(medicalRow, medicalOperation);

            _medicalOperationRepository.Update(medicalOperation);
        }
    }
}