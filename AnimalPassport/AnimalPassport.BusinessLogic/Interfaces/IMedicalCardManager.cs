using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AnimalPassport.BusinessLogic.DataTransferObjects;

namespace AnimalPassport.BusinessLogic.Interfaces
{
    public interface IMedicalCardManager
    {
        Task<IEnumerable<MedicalOperationDto>> GetAnimalMedRowsAsync(Guid animalId);

        Task<Guid> AddMedicalCardRowAsync(Guid animalId, MedicalRowDto medicalRow);

        Task UpdateMedicalCardRowAsync(Guid medicalCardId, MedicalRowDto medicalRow);

        Task DeleteMedicalRowAsync(Guid medicalRowId);
    }
}