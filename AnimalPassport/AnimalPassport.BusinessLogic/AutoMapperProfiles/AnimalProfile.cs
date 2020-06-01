using AnimalPassport.BusinessLogic.DataTransferObjects;
using AnimalPassport.BusinessLogic.DataTransferObjects.Animal;
using AnimalPassport.Entities.Entities;
using AutoMapper;
using System;

namespace AnimalPassport.BusinessLogic.AutoMapperProfiles
{
    public class AnimalProfile : Profile
    {
        public AnimalProfile()
        {
            CreateMap<Animal, AnimalModel>();
            CreateMap<Animal, AnimalInfo>();

            CreateMap<AnimalCreate, Animal>();

            CreateMap<MedicalOperation, MedicalOperationDto>();

            CreateMap<MedicalRowDto, MedicalOperation>()
                .ForMember(x => x.DateExpiry, opt => opt.Ignore());

            CreateMap<Attachment, AttachmentDto>();
        }
    }
}