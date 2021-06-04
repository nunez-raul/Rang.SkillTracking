using Rang.SkillTracking.Domain.Skills;
using System;

namespace Rang.SkillTracking.Domain.Employees
{
    public class Employee : BaseEntity
    {
        // fields

        // properties
        public uint Number { get; private set; }
        public string Name { get; private set; }
        public PersonalProfile Profile { get; private set; }
        public Evaluatee Evaluatee { get; private set; }
        public SkillEvaluator SkillEvaluator { get; private set; }

        // constructors
        public Employee(uint number, string name)
            :base()
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Number = number;
            Name = name;
            Profile = new PersonalProfile(this);
            Evaluatee = new Evaluatee(this);
            SkillEvaluator = new SkillEvaluator(this);
        }

        // methods
    }
}
