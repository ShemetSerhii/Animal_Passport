using System;
using System.Threading.Tasks;
using AnimalPassport.BusinessLogic.DataTransferObjects.Animal;
using AnimalPassport.BusinessLogic.Interfaces;
using AnimalPassport.WebApi.Extensions;
using AnimalPassport.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnimalPassport.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalManager _animalManager;

        public AnimalController(IAnimalManager animalManager)
        {
            _animalManager = animalManager;
        }

        [HttpGet("{ownerId}/animals")]
        public async Task<IActionResult> GetAnimals(Guid ownerId)
        {
            var animals = await _animalManager.GetAnimalsAsync(ownerId);

            return Ok(animals);
        }

        [HttpGet("{animalId}")]
        public async Task<IActionResult> GetAnimal(Guid animalId)
        {
            var animal = await _animalManager.GetAnimalAsync(animalId);

            return Ok(animal);
        }

        [HttpPost("{ownerId}")]
        public async Task<IActionResult> AddAnimal(Guid ownerId, [FromBody] AnimalCreate animal)
        {
            var animalId = await _animalManager.AddAnimalAsync(ownerId, animal);

            return Ok(animalId);
        }

        [HttpPut("{animalId}")]
        public async Task<IActionResult> UpdateAnimal(Guid animalId, [FromBody] AnimalCreate animal)
        {
            await _animalManager.UpdateAnimalAsync(animalId, animal);

            return Ok();
        }

        [HttpDelete("{animalId}")]
        public async Task<IActionResult> DeleteAnimal(Guid animalId)
        {
            await _animalManager.DeleteAnimalAsync(animalId);

            return Ok();
        }

        [HttpPost("{animalId}/picture")]
        public async Task<IActionResult> AddAnimalPicture(Guid animalId, [FromForm] PictureModel file)
        {
            await _animalManager.AddAnimalPictureAsync(animalId, file.Picture.AsFile());

            return Ok();
        }
    }
}