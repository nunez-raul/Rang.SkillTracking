using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Domain.Skills
{
    public class SkillGoal
    {
        // fields

        // properties
        public Evaluation Evaluation { get; private set; }
        public EvaluationPeriod EvaluationPeriod { get; private set; }
        public Evaluatee Evaluatee { get; private set; }
        public SkillEvaluator SkillEvaluator { get; private set; }
        public SkillScore SkillScore { get; private set; }

        // constructors
        public SkillGoal(Evaluation evaluation, SkillEvaluator skillEvaluator)
        {
            Evaluation = evaluation ?? throw new ArgumentNullException(nameof(evaluation));

            EvaluationPeriod = evaluation.EvaluationPeriod;
            Evaluatee = evaluation.Evaluatee;
            SkillEvaluator = skillEvaluator ?? throw new ArgumentNullException(nameof(skillEvaluator));
            SkillScore = new SkillScore();
        }

        // methods
    }
}
