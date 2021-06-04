using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using System;

namespace Rang.SkillTracking.Domain.Skills
{
    public class PersonalSkill : BaseEntity
    {
        // fields

        // properties
        public Skill Skill { get; private set; }
        public PersonalProfile Profile { get; private set; }
        public SkillLevel SkillLevel { get; private set; }

        // constructors
        public PersonalSkill(Skill skill, SkillLevel currentSkillLevel, PersonalProfile profile)
            :base()
        {
            Skill = skill ?? throw new ArgumentNullException(nameof(skill));
            SkillLevel = currentSkillLevel;

            Profile = profile ?? throw new ArgumentNullException(nameof(profile));
        }

        // methods
        public EntityOperationResult<PersonalSkill> SetSkillLevel(SkillLevel skillLevel)
        {
            SkillLevel = skillLevel;

            return new EntityOperationResult<PersonalSkill>(OperationStatusCode.Success, this);
        }
    }
}
