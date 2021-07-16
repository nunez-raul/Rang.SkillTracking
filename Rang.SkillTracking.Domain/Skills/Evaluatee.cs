using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rang.SkillTracking.Domain.Skills
{
    public class Evaluatee : BaseEntity<EvaluateeModel>
    {
        // fields
        protected List<Evaluation> _evaluations;

        // properties
        public Employee Employee { get; private set; }
        public IEnumerable<Evaluation> Evaluations { get { return _evaluations; } }

        // constructors
        public Evaluatee(Employee employee)
            :base(new EvaluateeModel())
        {
            Employee = employee ?? throw new ArgumentNullException(nameof(employee));
            _evaluations = new List<Evaluation>();

            _model.EmployeeModel = employee.GetModel();
            _model.EvaluationModels = new List<EvaluationModel>();
        }

        public Evaluatee(EvaluateeModel evaluateeModel)
            : base(evaluateeModel)
        {
            var employee = new Employee(evaluateeModel.EmployeeModel);

            Employee = employee;
            _evaluations = new List<Evaluation>();

            if(_model.EmployeeModel == null)
                _model.EmployeeModel = employee.GetModel();
            
            _model.EvaluationModels = new List<EvaluationModel>();
        }

        // methods
        public EntityOperationResult<Evaluation> AddNewEvaluation(EvaluationPeriod evaluationPeriod)
        {
            if (evaluationPeriod == null)
                throw new ArgumentNullException(nameof(evaluationPeriod));

            var evaluation = new Evaluation(this, evaluationPeriod);
            _evaluations.Add(evaluation);
            
            return new EntityOperationResult<Evaluation>(OperationStatusCode.Success, evaluation);
        }

        public PersonalSkill GetPersonalSkillFromProfile(Skill skill)
        {
            return Employee.Profile.Skills
                .Where(ps => ps.Skill.GetModel().Id.Equals(skill.GetModel().Id))
                .SingleOrDefault();
        }

        public override EvaluateeModel GetModel()
        {
            _model.EvaluationModels = _evaluations.Select(eval => eval.GetModel()).ToList();
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
