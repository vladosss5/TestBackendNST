using System;
using System.Collections.Generic;

namespace Core.Models;

public partial class PersonsSkill
{
    public long IdEntry { get; set; }

    public long IdPerson { get; set; }

    public int IdSkill { get; set; }

    public short Level { get; set; }

    public virtual Person IdPersonNavigation { get; set; } = null!;

    public virtual Skill IdSkillNavigation { get; set; } = null!;
}
