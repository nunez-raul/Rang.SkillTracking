using Rang.SkillTracking.Domain.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Domain.Employees
{
    public class Employee
    {
        // fields

        // properties
        public PersonalProfile Profile { get; private set; }
        public Evaluatee Evaluatee { get; private set; }
        public SkillEvaluator SkillEvaluator { get; private set; }

        // constructors
        public Employee()
        {
            Profile = new PersonalProfile();
            Evaluatee = new Evaluatee(this);
            SkillEvaluator = new SkillEvaluator(this);
        }

        // methods
    }
}
