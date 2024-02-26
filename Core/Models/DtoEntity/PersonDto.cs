namespace Core.Models.DtoEntity;

public class PersonDto
{
    public string Name { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public virtual ICollection<PersonsSkillDto> PersonsSkillDto { get; set; } = new List<PersonsSkillDto>();
}