using AutoMapper;
using Personnel.Application.Services.DTO;
using Personnel.Domain.Entities;

namespace Personnel.Application.Services.Mapping;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, PersonDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FullName.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.FullName.LastName))
            .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.FullName.MiddleName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone.Value));

        CreateMap<PersonDto, Person>()
            .ConstructUsing(dto => new Person(
                dto.LastName,
                dto.FirstName,
                dto.MiddleName,
                dto.Email,
                dto.Phone,
                dto.BirthDate,
                dto.Gender,
                dto.AvatarUrl,
                dto.Comment
            ));
    }
}