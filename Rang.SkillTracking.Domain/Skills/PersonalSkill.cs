using Rang.SkillTracking.Domain.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Domain.Skills
{
    public class PersonalSkill
    {
        // fields

        // properties
        public Skill Skill { get; private set; }
        public PersonalProfile Profile { get; private set; }

        // constructors
        public PersonalSkill(Skill skill, PersonalProfile profile)
        {
            Skill = skill ?? throw new ArgumentNullException(nameof(skill));
            Profile = profile ?? throw new ArgumentNullException(nameof(profile));
        }

        // methods
    }
}
