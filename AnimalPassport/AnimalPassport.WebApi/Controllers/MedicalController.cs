using System;
using System.Threading.Tasks;
using AnimalPassport.BusinessLogic.DataTransferObjects;
using AnimalPassport.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnimalPassport.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalController : ControllerBase
    {
        private readonly IMedicalCardManager _medicalCardManager;

        public MedicalController(IMedicalCardManager medicalCardManager)
        {
            _medicalCardManager = medicalCardManager;
        }


        [HttpGet("{animalId}")]
        public async Task<IActionResult> GetAnimalMed(Guid animalId)
        {
            var medRows = await _medicalCardManager.GetAnimalMedRowsAsync(animalId);

            return Ok(medRows);
        }

        [HttpPost("{animalId}")]
        public async Task<IActionResult> AddMedicalOperation(Guid animalId, [FromBody] MedicalRowDto medicalRow)
        {
            var id = await _medicalCardManager.AddMedicalCardRowAsync(animalId, medicalRow);

            return Ok(id);
        }

        [HttpPut("{animalId}")]
        public async Task<IActionResult> UpdateAnimalMed(Guid animalId, [FromBody] MedicalRowDto medicalRow)
        {
            await _medicalCardManager.UpdateMedicalCardRowAsync(animalId, medicalRow);

            return Ok();
        }
    }
}