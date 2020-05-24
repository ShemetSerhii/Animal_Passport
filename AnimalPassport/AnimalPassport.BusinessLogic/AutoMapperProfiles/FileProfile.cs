using AnimalPassport.BusinessLogic.DataTransferObjects;
using AnimalPassport.DataAccess.Blob.Models;
using AutoMapper;

namespace AnimalPassport.BusinessLogic.AutoMapperProfiles
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<FileDto, FileModel>();

            CreateMap<FileModel, FileDto>();
        }
    }
}