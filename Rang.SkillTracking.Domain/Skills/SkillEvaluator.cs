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

        // properties
        public Employee Employee { get; private set; }
        public IEnumerable<SkillGoal> SkillGoals { get => _skillGoals;  }

        // constructors
        public SkillEvaluator(Employee employee)
        {
            Employee = employee ?? throw new ArgumentNullException(nameof(employee));
            _skillGoals = new List<SkillGoal>();
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
    }
}
