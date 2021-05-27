using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Domain.Skills
{
    public class SkillScore
    {
        // fields

        // properties
        public SkillGoal SkillGoal { get; private set; }

        // constructors
        public SkillScore(SkillGoal skillGoal)
        {
            SkillGoal = skillGoal ?? throw new ArgumentNullException(nameof(skillGoal));
        }

        // methods
    }
}
