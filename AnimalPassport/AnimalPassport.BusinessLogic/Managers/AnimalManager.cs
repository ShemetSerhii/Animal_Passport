using AnimalPassport.BusinessLogic.DataTransferObjects;
using AnimalPassport.BusinessLogic.DataTransferObjects.Animal;
using AnimalPassport.BusinessLogic.Interfaces;
using AnimalPassport.DataAccess.Blob.Interfaces;
using AnimalPassport.DataAccess.Blob.Models;
using AnimalPassport.DataAccess.Interfaces;
using AnimalPassport.Entities.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalPassport.BusinessLogic.Managers
{
    public class AnimalManager : IAnimalManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Animal> _animalRepository;
        private readonly IMapper _mapper;
        private readonly IPictureBlobManager _pictureBlobManager;

        public AnimalManager(
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IPictureBlobManager pictureBlobManager)
        {
            _unitOfWork = unitOfWork;
            _animalRepository = unitOfWork.GetRepository<Animal>();
            _mapper = mapper;
            _pictureBlobManager = pictureBlobManager;
        }

        public async Task<AnimalInfo> GetAnimalAsync(Guid animalId)
        {
            var animal = await _animalRepository.GetAsync(animalId);

            var animalDto = _mapper.Map<AnimalInfo>(animal);

            if (!string.IsNullOrEmpty(animalDto.PicturePath))
            {
                animalDto.Picture = (await _pictureBlobManager.DownloadFileAsync(animal.PicturePath)).Content;
            }

            return animalDto;
        }

        public async Task<IEnumerable<AnimalModel>> GetAnimalsAsync(Guid ownerId)
        {
            var animals = await _animalRepository.GetAsync(a => a.OwnerId == ownerId);

            var animalsDto = _mapper.Map<IEnumerable<AnimalModel>>(animals).ToList();

            foreach (var animal in animalsDto)
            {
                if (!string.IsNullOrEmpty(animal.PicturePath))
                {
                    animal.Picture = (await _pictureBlobManager.DownloadFileAsync(animal.PicturePath)).Content;
                }
            }

            return animalsDto;
        }

        public async Task<Guid> AddAnimalAsync(Guid ownerId, AnimalCreate animalModel)
        {
            var animal = _mapper.Map<Animal>(animalModel);
            animal.OwnerId = ownerId;

            var id = _animalRepository.Create(animal);

            await _unitOfWork.SaveChangesAsync();

            return id;
        }

        public async Task UpdateAnimalAsync(Guid animalId, AnimalCreate animalDto)
        {
            var animal = await _animalRepository.GetAsync(animalId);

            _mapper.Map(animalDto, animal);

            _animalRepository.Update(animal);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddAnimalPictureAsync(Guid animalId, FileDto picture)
        {
            var animal = await _animalRepository.GetAsync(animalId);

            if (!string.IsNullOrEmpty(animal.PicturePath))
            {
                await _pictureBlobManager.DeleteFileAsync(animal.PicturePath);
            }

            var file = _mapper.Map<FileModel>(picture);

            file.FilePath = Path.Combine($"{animalId}", picture.FileName);
            animal.PicturePath = file.FilePath;

            await _pictureBlobManager.UploadFileAsync(file);

            _animalRepository.Update(animal);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}