using System.ComponentModel.DataAnnotations;

namespace App.Core.Models.DTOs;

public class SkillResponse
{
    public string Name { get; set; } = null!;

    [Range(1, 10, ErrorMessage = "Values 1-10 are accepted ")]
    public short Level { get; set; }
}