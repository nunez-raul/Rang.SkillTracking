using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using Rang.SkillTracking.Domain.Skills;
using System;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.Rang.SkillTracking.Domain.UnitTests
{
    public class EvaluationTests
    {
        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenAddNewSkillGoalIfSkillIsNull()
        {
            // arrange
            var evaluation = new Evaluation(new Employee().Evaluatee, new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31)));
            var skillEvaluator = new Employee().SkillEvaluator;
            var targetSkillLevel = SkillLevel.Advanced;
            var currentSkillLevel = SkillLevel.Average;

            // act
            void action() => evaluation.AddNewSkillGoal(null, skillEvaluator, targetSkillLevel, currentSkillLevel);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenAddNewSkillGoalIfSkillEvaluatorIsNull()
        {
            // arrange
            var evaluation = new Evaluation(new Employee().Evaluatee, new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31)));
            var skill = new Skill("C#");
            var targetSkillLevel = SkillLevel.Advanced;
            var currentSkillLevel = SkillLevel.Average;

            // act
            void action() => evaluation.AddNewSkillGoal(skill, null, targetSkillLevel, currentSkillLevel);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldAddNewSkillGoal()
        {
            // arrange
            var evaluation = new Evaluation(new Employee().Evaluatee, new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31)));
            var skill = new Skill("C#");
            var skillEvaluator = new Employee().SkillEvaluator;
            var targetSkillLevel = SkillLevel.Advanced;
            var currentSkillLevel = SkillLevel.Average;

            // act
            var result = evaluation.AddNewSkillGoal(skill, skillEvaluator, targetSkillLevel, currentSkillLevel);

            // assert
            Assert.Equal(OperationStatusCode.Success, result.OperationStatusCode);
            Assert.Single(evaluation.SkillGoals);
        }
    }
}
