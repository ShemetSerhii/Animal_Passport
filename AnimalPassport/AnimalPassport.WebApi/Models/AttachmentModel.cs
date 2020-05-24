using Microsoft.AspNetCore.Http;

namespace AnimalPassport.WebApi.Models
{
    public class AttachmentModel
    {
        public IFormFile Attachment { get; set; }
    }
}