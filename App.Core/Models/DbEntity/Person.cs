using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class Person
{
    public long IdPerson { get; set; }

    public string Name { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
