using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FirstRespository.Api.Data;
using FirstRespository.Api.Dtos.Person;

namespace FirstRespository.Api.Mappers
{
    public sealed class PersonMapper : Profile
    {
        public PersonMapper()
        {
            CreateMap<PersonModel, PersonIndexDto>()
                .ForMember(destination => destination.FullName, options => options.MapFrom(source => $"{source.FirstName} {source.LastName}"));

            CreateMap<PersonModel, PersonDetailDto>()
                .ForMember(destination => destination.FullName, options => options.MapFrom(source => $"{source.FirstName} {source.LastName}"))
                .ForMember(destination => destination.BirthDateYear, options => options.MapFrom(source => DateTime.Now.AddYears(-source.Age).Year));

            CreateMap<CreatePersonDto, PersonModel>();
            
            CreateMap<EditPersonDto, PersonModel>()
                .ForMember(destination=>destination.Id , options=> options.Ignore());
        }
    }
}
