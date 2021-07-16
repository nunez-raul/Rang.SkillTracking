﻿using Rang.SkillTracking.Domain.Common;
using System;
using System.Collections.Generic;

namespace Rang.SkillTracking.Domain.Skills
{
    public class Evaluation : BaseEntity<EvaluationModel>
    {
        // fields
        protected List<SkillGoal> _skillGoals;

        // properties
        public Evaluatee Evaluatee {get; private set;}
        public EvaluationPeriod EvaluationPeriod { get; private set; } 
        public IEnumerable<SkillGoal> SkillGoals { get => _skillGoals; }

        // constructors
        public Evaluation(Evaluatee evaluatee, EvaluationPeriod evaluationPeriod)
            :base(new EvaluationModel())
        {
            Evaluatee = evaluatee ?? throw new ArgumentNullException(nameof(evaluatee));
            EvaluationPeriod = evaluationPeriod ?? throw new ArgumentNullException(nameof(evaluationPeriod));
            _skillGoals = new List<SkillGoal>();

            _model.EvaluateeModel = evaluatee.GetModel();
            _model.EvaluationPeriodModel = evaluationPeriod.GetModel();
            _model.SkillGoalModels = new List<SkillGoalModel>();
        }

        // methods
        public EntityOperationResult<SkillGoal> AddNewSkillGoal(Skill skill, SkillEvaluator skillEvaluator, SkillLevel targetSkillLevel, SkillLevel currentSkillLevel)
        {
            var newSkillGoal = new SkillGoal(skill, skillEvaluator, targetSkillLevel, currentSkillLevel, this);
            _skillGoals.Add(newSkillGoal);

            return new EntityOperationResult<SkillGoal>(OperationStatusCode.Success, newSkillGoal);
        }
        public override EvaluationModel GetModel()
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
