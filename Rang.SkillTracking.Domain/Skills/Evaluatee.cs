using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Domain.Skills
{
    public class Evaluatee
    {
        // fields
        protected List<Evaluation> _evaluations;

        // properties
        public Employee Employee { get; private set; }
        public IEnumerable<Evaluation> Evaluations { get { return _evaluations; } }

        // constructors
        public Evaluatee(Employee employee, Evaluation[] evaluations = null)
        {
            Employee = employee ?? throw new ArgumentNullException(nameof(employee));
            _evaluations = evaluations == null
                ? new List<Evaluation>()
                : evaluations.ToList();
        }

        // methods
        public OperationStatusCode AddNewEvaluation(Evaluation evaluation)
        {
            if (evaluation == null)
                throw new ArgumentNullException(nameof(evaluation));

            _evaluations.Add(evaluation);
            return OperationStatusCode.Success;
        }
    }
}
