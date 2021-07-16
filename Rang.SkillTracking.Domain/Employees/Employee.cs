using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Skills;
using System;

namespace Rang.SkillTracking.Domain.Employees
{
    public class Employee : BaseEntity<EmployeeModel>
    {
        // fields

        // properties
        public uint Number { get => _model.Number; private set => _model.Number = value; }
        public string Name { get => _model.Name; private set => _model.Name = value; }
        public PersonalProfile Profile { get; private set; }
        public Evaluatee Evaluatee { get; private set; }
        public SkillEvaluator SkillEvaluator { get; private set; }

        // constructors
        public Employee(uint number, string name, bool isActive = false)
            :base(new EmployeeModel { Number = number, Name = name, IsActive = isActive })
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Profile = new PersonalProfile(this);
            
            Evaluatee = new Evaluatee(this);
            _model.EvaluateeModel = Evaluatee.GetModel();

            SkillEvaluator = new SkillEvaluator(this);
        }

        public Employee(EmployeeModel employeeModel)
            :base(employeeModel)
        {


            Profile = new PersonalProfile(this);
            Evaluatee = new Evaluatee(this);
            SkillEvaluator = new SkillEvaluator(this);
        }

        // methods
        public override EmployeeModel GetModel()
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
