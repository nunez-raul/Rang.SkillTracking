using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using Rang.SkillTracking.Domain.Skills;
using System;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.Rang.SkillTracking.Tests.xUnit.Domain.Skills
{
    public class EvaluationTests
    {
        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenAddNewSkillGoalWithNullSkill()
        {
            // arrange
            var evaluation = new Evaluation(new Employee().Evaluatee, new EvaluationPeriod());
            var skillEvaluator = new Employee().SkillEvaluator;

            // act
            void action() => evaluation.AddNewSkillGoal(null, skillEvaluator, SkillLevel.Advanced, SkillLevel.Average);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenAddNewSkillGoalWithNullSkillEvaluator()
        {
            // arrange
            var evaluation = new Evaluation(new Employee().Evaluatee, new EvaluationPeriod());
            var skill = new Skill("C#");

            // act
            void action() => evaluation.AddNewSkillGoal(skill, null, SkillLevel.Advanced, SkillLevel.Average);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldAddNewSkillGoal()
        {
            // arrange
            var evaluation = new Evaluation(new Employee().Evaluatee, new EvaluationPeriod());
            var skill = new Skill("C#");
            var skillEvaluator = new Employee().SkillEvaluator;

            // act
            var result = evaluation.AddNewSkillGoal(skill, skillEvaluator, SkillLevel.Advanced, SkillLevel.Average);

            // assert
            Assert.Equal(OperationStatusCode.Success, result);
            Assert.Single(evaluation.SkillGoals);
        }
    }
}
