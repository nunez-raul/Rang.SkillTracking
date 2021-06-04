using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rang.SkillTracking.Domain.Skills
{
    public class Evaluatee : BaseEntity
    {
        // fields
        protected List<Evaluation> _evaluations;

        // properties
        public Employee Employee { get; private set; }
        public IEnumerable<Evaluation> Evaluations { get { return _evaluations; } }

        // constructors
        public Evaluatee(Employee employee, Evaluation[] evaluations = null)
            :base()
        {
            Employee = employee ?? throw new ArgumentNullException(nameof(employee));
            _evaluations = evaluations == null
                ? new List<Evaluation>()
                : evaluations.ToList();
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
                .Where(ps => ps.Skill.Id.Equals(skill.Id))
                .SingleOrDefault();
        }
    }
}
