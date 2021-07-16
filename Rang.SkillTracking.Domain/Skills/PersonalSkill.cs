using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using System;

namespace Rang.SkillTracking.Domain.Skills
{
    public class PersonalSkill : BaseEntity<PersonalSkillModel>
    {
        // fields

        // properties
        public Skill Skill { get; private set; }
        public PersonalProfile Profile { get; private set; }
        public SkillLevel SkillLevel { get; private set; }

        // constructors
        public PersonalSkill(Skill skill, SkillLevel currentSkillLevel, PersonalProfile profile)
            :base(new PersonalSkillModel())
        {
            Skill = skill ?? throw new ArgumentNullException(nameof(skill));
            SkillLevel = currentSkillLevel;

            Profile = profile ?? throw new ArgumentNullException(nameof(profile));

            _model.SkillModel = skill.GetModel();
            _model.SkillLevel = currentSkillLevel;
            _model.ProfileModel = profile.GetModel();
        }

        // methods
        public EntityOperationResult<PersonalSkill> SetSkillLevel(SkillLevel skillLevel)
        {
            SkillLevel = skillLevel;

            return new EntityOperationResult<PersonalSkill>(OperationStatusCode.Success, this);
        }

        public override PersonalSkillModel GetModel()
        {
            return _model;
        }

        protected override void InitializeMe()
        {

        }

        protected override bool ValidateMe()
        {
            return true;
        }
    }
}
