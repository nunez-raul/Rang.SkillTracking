using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using System;

namespace Rang.SkillTracking.Domain.Skills
{
    public class PersonalSkill
    {
        // fields

        // properties
        public Skill Skill { get; private set; }
        public PersonalProfile Profile { get; private set; }
        public SkillLevel SkillLevel { get; private set; }

        // constructors
        public PersonalSkill(Skill skill, PersonalProfile profile)
        {
            Skill = skill ?? throw new ArgumentNullException(nameof(skill));
            Profile = profile ?? throw new ArgumentNullException(nameof(profile));
        }

        // methods
        public OperationStatusCode SetSkillLevel(SkillLevel skillLevel)
        {
            SkillLevel = skillLevel;

            return OperationStatusCode.Success;
        }
    }
}
