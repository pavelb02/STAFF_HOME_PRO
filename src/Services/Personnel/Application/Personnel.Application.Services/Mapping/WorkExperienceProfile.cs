using AutoMapper;
using Personnel.Application.Services.DTO;
using Personnel.Domain.Entities;

namespace Personnel.Application.Services.Mapping;

public class WorkExperienceProfile : Profile
{
    public WorkExperienceProfile()
    {
        CreateMap<WorkExperience, WorkExperienceDto>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country));

        CreateMap<WorkExperienceDto, WorkExperience>()
            .ConstructUsing(dto => new WorkExperience(
                dto.Position,
                dto.Organization,
                dto.City,
                dto.Country,
                dto.StartDate,
                dto.EndDate,
                dto.Description
            ));
    }
}