using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AnimalPassport.BusinessLogic.DataTransferObjects;
using AnimalPassport.BusinessLogic.DataTransferObjects.Animal;

namespace AnimalPassport.BusinessLogic.Interfaces
{
    public interface IAnimalManager
    {
        Task<IEnumerable<AnimalModel>> GetAnimalsAsync(Guid ownerId);

        Task<AnimalInfo> GetAnimalAsync(Guid animalId);

        Task<Guid> AddAnimalAsync(Guid ownerId, AnimalCreate animal);

        Task UpdateAnimalAsync(Guid animalId, AnimalCreate animal);

        Task DeleteAnimalAsync(Guid animalId);

        Task AddAnimalPictureAsync(Guid animalId, FileDto picture);
    }
}