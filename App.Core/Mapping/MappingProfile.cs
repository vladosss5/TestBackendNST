using App.Core.Models.DTOs;
using AutoMapper;
using Infrastructure;

namespace App.Core.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PersonRequest, Person>()
            .ForMember(dest => dest.Skills, opts => opts.MapFrom(src => src.Skills
                .Select(x => new Skill() { Name = x.Name, Level = x.Level })));

            CreateMap<Person, PersonRequest>()
            .ForMember(dest => dest.Skills, opts => opts.MapFrom(src => src.Skills
                .Select(x => new SkillRequest() { Name = x.Name, Level = x.Level })));

            CreateMap<PersonResponse, Person>()
            .ForMember(dest => dest.Skills, opts => opts.MapFrom(src => src.Skills
                .Select(x => new Skill() { Name = x.Name, Level = x.Level })));

            CreateMap<Person, PersonResponse>()
            .ForMember(dest => dest.Skills, opts => opts.MapFrom(src => src.Skills
                .Select(x => new SkillResponse() { Name = x.Name, Level = x.Level })));
    }
}