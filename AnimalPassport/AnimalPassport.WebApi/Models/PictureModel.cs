using Microsoft.AspNetCore.Http;

namespace AnimalPassport.WebApi.Models
{
    public class PictureModel
    {
        public IFormFile Picture { get; set; }
    }
}