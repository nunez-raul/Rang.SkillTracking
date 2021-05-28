using Rang.SkillTracking.Domain.Common;
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
        private readonly DateTime _date;
        private readonly List<SkillSnapshot> _skillSnapshots;

        // properties
        public SkillEvaluator Owner { get; private set; }
        public EvaluationPeriod EvaluationPeriod { get; private set; }
        public DateTime Date { get => _date.Date; } 
        public IEnumerable<SkillSnapshot> SkillSnapshots { get => _skillSnapshots; }

        // constructors
        public TrackingPoint(SkillEvaluator owner, EvaluationPeriod evaluationPeriod, DateTime date)
        {
            _skillSnapshots = new List<SkillSnapshot>();

            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            EvaluationPeriod = evaluationPeriod ?? throw new ArgumentNullException(nameof(evaluationPeriod));
            _date = date.Date;
        }

        // methods
        public OperationStatusCode AddNewSkillSnapshot(PersonalSkill personalSkill)
        {
            var skillSnapshot = new SkillSnapshot(this, personalSkill);
            _skillSnapshots.Add(skillSnapshot);

            return OperationStatusCode.Success;
        }
    }
}
