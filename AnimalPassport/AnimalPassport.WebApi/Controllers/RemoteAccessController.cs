using System.Threading.Tasks;
using AnimalPassport.BusinessLogic.Interfaces;
using AnimalPassport.WebApi.Hubs;
using AnimalPassport.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AnimalPassport.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemoteAccessController : ControllerBase
    {
        private readonly IAnimalManager _animalManager;
        private readonly IHubContext<RemoteAccessHub> _hub;

        public RemoteAccessController(IHubContext<RemoteAccessHub> hub, IAnimalManager animalManager)
        {
            _hub = hub;
            _animalManager = animalManager;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessId(Access access)
        {
            var animal = await _animalManager.GetAnimalAsync(access.Id);

            if (animal != null)
            {
                await _hub.Clients.All.SendAsync("process", animal.Id);

                return Ok("Операція прошла успішно");
            }

            return BadRequest($"Не вдалося здайти домашню тварину з ідентифікатором: {access.Id}");
        }
    }
}