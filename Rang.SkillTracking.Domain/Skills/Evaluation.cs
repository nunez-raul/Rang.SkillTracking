﻿using Rang.SkillTracking.Domain.Common;
using System;
using System.Collections.Generic;

namespace Rang.SkillTracking.Domain.Skills
{
    public class Evaluation
    {
        // fields
        protected List<SkillGoal> _skillGoals;

        // properties
        public Evaluatee Evaluatee {get; private set;}
        public EvaluationPeriod EvaluationPeriod { get; private set; } 
        public IEnumerable<SkillGoal> SkillGoals { get => _skillGoals; }

        // constructors
        public Evaluation(Evaluatee evaluatee, EvaluationPeriod evaluationPeriod)
        {
            Evaluatee = evaluatee ?? throw new ArgumentNullException(nameof(evaluatee));
            EvaluationPeriod = evaluationPeriod ?? throw new ArgumentNullException(nameof(evaluationPeriod));
            _skillGoals = new List<SkillGoal>();

            Evaluatee.AddNewEvaluation(this);
        }

        // methods
        public OperationStatusCode AddNewSkillGoal(SkillGoal skillGoal)
        {
            if (skillGoal == null)
                throw new ArgumentNullException(nameof(skillGoal));
            _skillGoals.Add(skillGoal);

            return OperationStatusCode.Success;
        }

        public OperationStatusCode AddNewSkillGoal(Skill skill, SkillEvaluator skillEvaluator, SkillLevel targetSkillLevel, SkillLevel currentSkillLevel)
        {
            var newSkillGoal = new SkillGoal(skill, skillEvaluator, targetSkillLevel, currentSkillLevel, this);
            _skillGoals.Add(newSkillGoal);

            return OperationStatusCode.Success;
        }
    }
}
