using AnimalPassport.BusinessLogic.DataTransferObjects;
using Microsoft.AspNetCore.Http;

namespace AnimalPassport.WebApi.Extensions
{
    public static class FormFileExtensions
    {
        public static FileDto AsFile(this IFormFile formFile)
        {
            using var stream = formFile.OpenReadStream();

            return new FileDto
            {
                FileName = formFile.FileName,
                ContentType = formFile.ContentType,
                Content = stream.GetBytes()
            };
        }
    }
}