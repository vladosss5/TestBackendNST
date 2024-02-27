namespace App.Core.Models.DTOs;

public class PersonResponse
{
    public string Name { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public virtual ICollection<SkillResponse> Skills { get; set; } = new List<SkillResponse>();
}