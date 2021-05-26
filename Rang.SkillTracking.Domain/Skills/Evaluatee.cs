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

        // properties
        public Employee Employee { get; private set; }

        // constructors
        public Evaluatee(Employee employee)
        {
            Employee = employee ?? throw new ArgumentNullException(nameof(employee));
        }

        // methods
    }
}
