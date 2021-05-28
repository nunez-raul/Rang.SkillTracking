using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Domain.Skills
{
    public class TrackingPoint
    {
        // fields
        private DateTime _date;

        // properties
        public SkillEvaluator Owner { get; private set; }
        public EvaluationPeriod EvaluationPeriod { get; private set; }
        public DateTime Date { get => _date.Date; } 

        // constructors
        public TrackingPoint(SkillEvaluator owner ,EvaluationPeriod evaluationPeriod, DateTime date)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            EvaluationPeriod = evaluationPeriod ?? throw new ArgumentNullException(nameof(evaluationPeriod));
            _date = date.Date;
        }

        // methods
    }
}
