using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rang.SkillTracking.Domain.Skills
{
    public class SkillEvaluator
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
        {
            Employee = employee ?? throw new ArgumentNullException(nameof(employee));
            _skillGoals = new List<SkillGoal>();
            _trackingPoints = new List<TrackingPoint>();
        }

        // methods
        public OperationStatusCode AddNewSkillGoal(Skill skill, SkillEvaluator skillEvaluator, SkillLevel targetLevel, SkillLevel currentLevel, Evaluation evaluation)
        {
            if (skill == null)
                throw new ArgumentNullException(nameof(skill));

            if (skillEvaluator == null)
                throw new ArgumentNullException(nameof(skillEvaluator));

            if (evaluation == null)
                throw new ArgumentNullException(nameof(evaluation));

            var skillGoal = new SkillGoal(skill, skillEvaluator, targetLevel, currentLevel, evaluation);
            _skillGoals.Add(skillGoal);

            return OperationStatusCode.Success;
        }

        public OperationStatusCode AddNewTrackingPoint(EvaluationPeriod evaluationPeriod, DateTime date)
        {
            _trackingPoints.Add(
                new TrackingPoint(this, evaluationPeriod ,  date));

            return OperationStatusCode.Success;
        }

        public OperationStatusCode AddNewSkillSnapshotToTrackingPoint(Evaluatee evaluatee, Skill skill, SkillLevel currentSkillLevel, DateTime date)
        {
            var trackingPoint =  _trackingPoints
                .Where(Tp => Tp.Date.Date.Equals(date.Date))
                .SingleOrDefault();

            if (trackingPoint == null)
                return OperationStatusCode.MissingTrackingPoint;

            trackingPoint.AddNewSkillSnapshot(
                new PersonalSkill(skill, currentSkillLevel, evaluatee.Employee.Profile));

            return OperationStatusCode.Success;
        }

        public OperationStatusCode SetSkillScoreToSkillGoal(SkillGoal skillGoal, SkillLevel skillLevelAchieved, int score, string note)
        {
            if (skillGoal == null)
                throw new ArgumentNullException(nameof(skillGoal));

            if (_skillGoals.Where(sg => sg.Id == skillGoal.Id).SingleOrDefault() == null)
                return OperationStatusCode.MissingSkillGoal;

            var skillScore = skillGoal.SkillScore;
            skillScore.SetScore(skillLevelAchieved, score, note);
            
            return OperationStatusCode.Success;
        }
    }
}
