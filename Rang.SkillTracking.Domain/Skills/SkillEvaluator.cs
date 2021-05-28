using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using System;
using System.Collections.Generic;

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
        public OperationStatusCode SetSkillScore(SkillGoal skillGoal, int score, string note)
        {
            _skillGoals.Add(skillGoal);

            var skillScore = skillGoal.SkillScore;
            skillScore.Score = score;
            skillScore.AddNote(note);

            return OperationStatusCode.Success;
        }

        public OperationStatusCode AddNewTrackingPoint(EvaluationPeriod evaluationPeriod, DateTime date)
        {
            _trackingPoints.Add(new TrackingPoint(this, evaluationPeriod ,  date));

            return OperationStatusCode.Success;
        }
    }
}
