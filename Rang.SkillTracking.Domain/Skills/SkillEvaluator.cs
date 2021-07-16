using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rang.SkillTracking.Domain.Skills
{
    public class SkillEvaluator : BaseEntity<SkillEvaluatorModel>
    {
        // fields
        protected List<SkillGoal> _skillGoals;
        protected List<TrackingPoint> _trackingPoints;

        // properties
        public Employee Employee { get; private set; }
        public IEnumerable<SkillGoal> SkillGoals { get => _skillGoals;  }
        public IEnumerable<TrackingPoint> TrackingPoints { get => _trackingPoints; }

        // constructors
        public SkillEvaluator(Employee employee)
            :base(new SkillEvaluatorModel())
        {
            Employee = employee ?? throw new ArgumentNullException(nameof(employee));
            _skillGoals = new List<SkillGoal>();
            _trackingPoints = new List<TrackingPoint>();

            _model.EmployeeModel = employee.GetModel();
            _model.SkillGoalModels = new List<SkillGoalModel>();
            _model.TrackingPointModels = new List<TrackingPointModel>();
        }

        // methods
        public EntityOperationResult<SkillGoal> AddNewSkillGoal(Skill skill, SkillEvaluator skillEvaluator, SkillLevel targetLevel, SkillLevel currentLevel, Evaluation evaluation)
        {
            if (skill == null)
                throw new ArgumentNullException(nameof(skill));

            if (skillEvaluator == null)
                throw new ArgumentNullException(nameof(skillEvaluator));

            if (evaluation == null)
                throw new ArgumentNullException(nameof(evaluation));

            var skillGoal = new SkillGoal(skill, skillEvaluator, targetLevel, currentLevel, evaluation);
            _skillGoals.Add(skillGoal);

            return new EntityOperationResult<SkillGoal>(OperationStatusCode.Success, skillGoal);
        }

        public EntityOperationResult<TrackingPoint> AddNewTrackingPoint(EvaluationPeriod evaluationPeriod, DateTime date)
        {
            var trackingPoint = new TrackingPoint(this, evaluationPeriod, date);
            _trackingPoints.Add(trackingPoint);

            return new EntityOperationResult<TrackingPoint>(OperationStatusCode.Success, trackingPoint);
        }

        public EntityOperationResult<SkillSnapshot> AddNewSkillSnapshotToTrackingPoint(Evaluatee evaluatee, Skill skill, SkillLevel currentSkillLevel, DateTime date)
        {
            var trackingPoint =  _trackingPoints
                .Where(Tp => Tp.Date.Date.Equals(date.Date))
                .SingleOrDefault();

            if (trackingPoint == null)
                return new EntityOperationResult<SkillSnapshot>(OperationStatusCode.MissingTrackingPoint);

            var result = trackingPoint.AddNewSkillSnapshot(
                new PersonalSkill(skill, currentSkillLevel, evaluatee.Employee.Profile));

            return new EntityOperationResult<SkillSnapshot>(OperationStatusCode.Success, result.EntityCollection.First());
        }

        public EntityOperationResult<SkillScore> SetSkillScoreToSkillGoal(SkillGoal skillGoal, SkillLevel skillLevelAchieved, int score, string note)
        {
            if (skillGoal == null)
                throw new ArgumentNullException(nameof(skillGoal));

            if (_skillGoals.Where(sg => sg.GetModel().Id == skillGoal.GetModel().Id).SingleOrDefault() == null)
                return new EntityOperationResult<SkillScore>(OperationStatusCode.MissingSkillGoal);

            var skillScore = skillGoal.SkillScore;
            skillScore.SetScore(skillLevelAchieved, score, note);
            
            return new EntityOperationResult<SkillScore>(OperationStatusCode.Success, skillScore);
        }

        public override SkillEvaluatorModel GetModel()
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
