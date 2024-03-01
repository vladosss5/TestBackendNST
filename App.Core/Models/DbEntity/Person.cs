using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class Person
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
