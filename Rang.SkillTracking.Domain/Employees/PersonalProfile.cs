using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Skills;
using System;
using System.Collections.Generic;

namespace Rang.SkillTracking.Domain.Employees
{
    public class PersonalProfile : BaseEntity<PersonalProfileModel>
    {
        // fields
        private readonly IList<PersonalSkill> _skills;

        // properties
        public IEnumerable<PersonalSkill> Skills { get => _skills; }
        public Employee Employee { get; private set; }

        // constructors
        public PersonalProfile(Employee employee)
            : base(new PersonalProfileModel())
        {
            Employee = employee ?? throw new ArgumentNullException(nameof(employee));
            _skills = new List<PersonalSkill>();

            _model.EmployeeModel = employee.GetModel();
            _model.SkillModels = new List<PersonalSkillModel>();
        }

        // methods
        public EntityOperationResult<PersonalSkill> AddNewPersonalSkill(Skill skill, SkillLevel currentLevel)
        {
            var personalSkill = new PersonalSkill(skill, currentLevel, this);
            _skills.Add(personalSkill);

            return new EntityOperationResult<PersonalSkill>(OperationStatusCode.Success, personalSkill);
        }

        public override PersonalProfileModel GetModel()
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
