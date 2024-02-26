using AutoMapper;
using Core.Models;
using Core.Models.DtoEntity;

namespace Core.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Person, PersonDto>();
        CreateMap<Skill, SkillDto>();
        CreateMap<PersonsSkill, PersonsSkillDto>();
    }
}