using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Domain.Employees
{
    public class PersonalProfile
    {
        // fields
        private readonly IList<PersonalSkill> _skills;

        // properties
        public IEnumerable<PersonalSkill> Skills { get => _skills; }
        public Employee Employee { get; private set; }

        // constructors
        public PersonalProfile(Employee employee)
        {
            Employee = employee ?? throw new ArgumentNullException(nameof(employee));
            _skills = new List<PersonalSkill>();
        }

        // methods
        public OperationStatusCode AddNewPersonalSkill(Skill skill, SkillLevel currentLevel)
        {
            _skills.Add(new PersonalSkill(skill, currentLevel, this));

            return OperationStatusCode.Success;
        }
    }
}
