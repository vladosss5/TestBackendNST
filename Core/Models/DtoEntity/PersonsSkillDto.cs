namespace Core.Models.DtoEntity;

public class PersonsSkillDto
{
    public short Level { get; set; }

    public virtual PersonDto IdPersonNavigation { get; set; } = null!;

    public virtual SkillDto IdSkillNavigation { get; set; } = null!;
}