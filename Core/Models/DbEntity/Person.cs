using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class Person
{
    public long IdPerson { get; set; }

    public string Name { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public virtual ICollection<PersonsSkill> PersonsSkills { get; set; } = new List<PersonsSkill>();
}
