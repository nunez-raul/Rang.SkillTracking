﻿using Rang.SkillTracking.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rang.SkillTracking.Domain.Skills
{
    public class Evaluation
    {
        // fields
        protected List<SkillGoal> _skillGoals;

        // properties
        public Evaluatee Evaluatee {get; private set;}
        public EvaluationPeriod EvaluationPeriod { get; private set; } 
        public IEnumerable<SkillGoal> SkillGoals { get { return _skillGoals; }}

        // constructors
        public Evaluation(Evaluatee evaluatee, EvaluationPeriod evaluationPeriod, SkillGoal[] skillGoals = null)
        {
            Evaluatee = evaluatee ?? throw new ArgumentNullException(nameof(evaluatee));
            EvaluationPeriod = evaluationPeriod ?? throw new ArgumentNullException(nameof(evaluationPeriod));
            _skillGoals = skillGoals == null 
                ? new List<SkillGoal>() 
                : skillGoals.ToList();
        }

        // methods
        public OperationStatusCode AddNewSkillGoal(Skill skill, SkillEvaluator skillEvaluator, SkillLevel targetSkillLevel)
        {
            var newSkillGoal = new SkillGoal(skill, skillEvaluator, targetSkillLevel, this);
            _skillGoals.Add(newSkillGoal);

            return OperationStatusCode.Success;
        }
    }
}
