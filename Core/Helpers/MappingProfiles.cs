using AutoMapper;
using Core.Models;
using Core.Models.DtoEntity;

namespace Core.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Person, PersonDto>();
        CreateMap<PersonDto, Person>();
        CreateMap<Skill, SkillDto>();
        CreateMap<SkillDto, Skill>();
        // CreateMap<PersonsSkill, PersonsSkillDto>();
    }
}