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
        public OperationStatusCode SetSkillScore(Evaluatee evaluatee, Skill skill, EvaluationPeriod evaluationperiod, SkillLevel currentSkillLevel , int score, string note)
        {
            var evaluation = evaluatee.Evaluations
                .Where(e => e.EvaluationPeriod.Equals(evaluationperiod))
                .FirstOrDefault();

            if (evaluation == null)
            {
                throw new NotImplementedException();
            }

            var skillGoal = evaluation.SkillGoals
                .Where(sg => sg.PersonalSkill.Skill.Equals(skill))
                .FirstOrDefault();

            if(skillGoal == null)
            {
                throw new NotImplementedException();
            }

            skillGoal.PersonalSkill.SetSkillLevel(currentSkillLevel);

            _skillGoals.Add(skillGoal);

            var skillScore = skillGoal.SkillScore;
            skillScore.Score = score;
            skillScore.AddNote(note);
            

            return OperationStatusCode.Success;
        }

        public OperationStatusCode AddNewSkillGoal(SkillGoal skillGoal)
        {
            if (skillGoal == null)
                throw new ArgumentNullException(nameof(skillGoal));

            _skillGoals.Add(skillGoal);

            return OperationStatusCode.Success;
        }

        public OperationStatusCode AddNewTrackingPoint(EvaluationPeriod evaluationPeriod, DateTime date)
        {
            _trackingPoints.Add(
                new TrackingPoint(this, evaluationPeriod ,  date));

            return OperationStatusCode.Success;
        }

        public OperationStatusCode AddNewSkillSnapshot(Evaluatee evaluatee, Skill skill, SkillLevel currentSkillLevel, DateTime date)
        {
            var trackingPoint =  _trackingPoints
                .Where(Tp => Tp.Date.Date.Equals(date.Date))
                .SingleOrDefault();
            
            trackingPoint.AddNewSkillSnapshot(
                new PersonalSkill(skill, currentSkillLevel, evaluatee.Employee.Profile));

            return OperationStatusCode.Success;
        }
    }
}
