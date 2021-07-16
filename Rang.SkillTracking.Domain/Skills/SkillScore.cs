using Rang.SkillTracking.Domain.Common;
using System;
using System.Collections.Generic;

namespace Rang.SkillTracking.Domain.Skills
{
    public class SkillScore : BaseEntity<SkillScoreModel>
    {
        // fields
        protected List<string> _notes;

        // properties
        public SkillGoal SkillGoal { get; private set; }
        public SkillSnapshot AchievedSkillLevel { get; private set; }
        public int Score { get => _model.Score; private set => _model.Score = value; }
        public IEnumerable<string> Notes { get => _notes; }

        // constructors
        public SkillScore(SkillGoal skillGoal)
            :base(new SkillScoreModel())
        {
            SkillGoal = skillGoal ?? throw new ArgumentNullException(nameof(skillGoal));
            AchievedSkillLevel = null;
            _notes = new List<string>();

            _model.SkillGoalModel = skillGoal.GetModel();
            _model.AchievedSkillLevelModel = null;
            //_model.Notes = new List<string>();
        }

        // methods
        public void SetScore(SkillLevel skillLevelAchieved, int score, string note = null)
        {
            AchievedSkillLevel = new SkillSnapshot(
                new TrackingPoint(SkillGoal.SkillEvaluator, SkillGoal.EvaluationPeriod, SkillGoal.EvaluationPeriod.EndDate),
                new PersonalSkill(SkillGoal.InitialSkillLevel.PersonalSkill.Skill, skillLevelAchieved, SkillGoal.Evaluatee.Employee.Profile));

            Score = score;

            if(!string.IsNullOrWhiteSpace(note))
            {
                _notes.Add(note);
            }
        }

        public override SkillScoreModel GetModel()
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
