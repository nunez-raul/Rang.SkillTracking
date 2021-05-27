﻿using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using Rang.SkillTracking.Domain.Skills;
using System;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.Rang.SkillTracking.Tests.xUnit.Domain.Skills
{
    public class EvaluateeTests
    {
        [Fact]
        public void ShouldAddNewEvaluation()
        {
            // arrange
            var evaluatee = new Employee().Evaluatee;
            var evaluation = new Evaluation(evaluatee, new EvaluationPeriod());

            // act
            var result = evaluatee.AddNewEvaluation(evaluation);

            // assert
            Assert.Equal(OperationStatusCode.Success, result);
            Assert.Single(evaluatee.Evaluations);
        }

        [Fact]
        public void ShouldThrowExceptionWhenAddNewEvaluationWithNullEvaluation()
        {
            // arrange
            var evaluatee = new Employee().Evaluatee;

            // act
            void action() => evaluatee.AddNewEvaluation(null);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }
    }
}
