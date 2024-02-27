using System;
using System.Collections.Generic;

namespace Infrastructure;

public partial class Skill
{
    public long IdSkill { get; set; }

    public string Name { get; set; } = null!;

    public short Level { get; set; }

    public long IdPerson { get; set; }

    public virtual Person IdPersonNavigation { get; set; } = null!;
}
