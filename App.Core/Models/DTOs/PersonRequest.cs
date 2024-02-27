namespace App.Core.Models.DTOs;

public class PersonRequest
{
    public string Name { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public ICollection<SkillRequest> Skills { get; set; } = new List<SkillRequest>();
}