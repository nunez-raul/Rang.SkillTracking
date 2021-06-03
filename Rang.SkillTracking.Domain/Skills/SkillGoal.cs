using Rang.SkillTracking.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Domain.Skills
{
    public class SkillGoal: BaseEntity
    {
        // fields

        // properties
        public SkillSnapshot InitialSkillLevel { get; private set; }
        public SkillLevel TargetSkillLevel { get; private set; }
        public Evaluation Evaluation { get; private set; }
        public EvaluationPeriod EvaluationPeriod { get; private set; }
        public Evaluatee Evaluatee { get; private set; }
        public SkillEvaluator SkillEvaluator { get; private set; }
        public SkillScore SkillScore { get; private set; }

        // constructors
        public SkillGoal(Skill skill, SkillEvaluator skillEvaluator, SkillLevel targetSkillLevel, SkillLevel currentSkillLevel, Evaluation evaluation)
            :base()
        {
            if(skill == null)
                throw new ArgumentNullException(nameof(skill));

            Evaluation = evaluation ?? throw new ArgumentNullException(nameof(evaluation));
            Evaluatee = Evaluation.Evaluatee;
            TargetSkillLevel = targetSkillLevel;
            EvaluationPeriod = evaluation.EvaluationPeriod;
            SkillEvaluator = skillEvaluator ?? throw new ArgumentNullException(nameof(skillEvaluator));
            SkillScore = new SkillScore(this);

            var existingPersonalSkill = Evaluatee.GetPersonalSkillFromProfile(skill);
            if (existingPersonalSkill  != null)
            {
                existingPersonalSkill.SetSkillLevel(currentSkillLevel);
                InitialSkillLevel = new SkillSnapshot(new TrackingPoint(SkillEvaluator, EvaluationPeriod, EvaluationPeriod.StartDate), existingPersonalSkill);
            }
            else
            {
                InitialSkillLevel = new SkillSnapshot(new TrackingPoint(SkillEvaluator, EvaluationPeriod, EvaluationPeriod.StartDate), new PersonalSkill(skill, currentSkillLevel, Evaluatee.Employee.Profile));
            }
            
        }

        // methods
    }
}
