using Rang.SkillTracking.Domain.Common;
using System;

namespace Rang.SkillTracking.Domain.Skills
{
    public class SkillGoal: BaseEntity<SkillGoalModel>
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
            :base(new SkillGoalModel())
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

            _model.EvaluationModel = evaluation.GetModel();
            _model.EvaluateeModel = Evaluation.Evaluatee.GetModel();
            _model.TargetSkillLevel = targetSkillLevel;
            _model.EvaluationPeriodModel = evaluation.EvaluationPeriod.GetModel();
            _model.SkillEvaluatorModel = skillEvaluator.GetModel();
            _model.SkillScoreModel = SkillScore.GetModel();
            _model.InitialSkillLevelModel = InitialSkillLevel.GetModel();
        }

        // methods
        public override SkillGoalModel GetModel()
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
