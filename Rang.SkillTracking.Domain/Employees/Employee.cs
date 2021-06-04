using Rang.SkillTracking.Domain.Skills;

namespace Rang.SkillTracking.Domain.Employees
{
    public class Employee : BaseEntity
    {
        // fields

        // properties
        public PersonalProfile Profile { get; private set; }
        public Evaluatee Evaluatee { get; private set; }
        public SkillEvaluator SkillEvaluator { get; private set; }

        // constructors
        public Employee()
            :base()
        {
            Profile = new PersonalProfile(this);
            Evaluatee = new Evaluatee(this);
            SkillEvaluator = new SkillEvaluator(this);
        }

        // methods
    }
}
