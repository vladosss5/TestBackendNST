using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class Skill
{
    public int IdSkill { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<PersonsSkill> PersonsSkills { get; set; } = new List<PersonsSkill>();
}
