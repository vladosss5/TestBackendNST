using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class Skill
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public short Level { get; set; }

    public long IdPerson { get; set; }

    public Person Person { get; set; } = null!;
}
