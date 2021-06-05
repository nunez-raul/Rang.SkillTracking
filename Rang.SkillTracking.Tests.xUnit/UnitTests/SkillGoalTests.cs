using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using Rang.SkillTracking.Domain.Skills;
using System;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.UnitTests
{
    public class SkillGoalTests
    {
        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenCreatingInstanceIfSkillIsNull()
        {
            // arrange
            var skillEvaluator = new Employee(100, "Jane Doe").SkillEvaluator;
            var targetLevel = SkillLevel.Advanced;
            var currentLevel = SkillLevel.Average;
            var evaluatee = new Employee(101, "John Doe").Evaluatee;
            var evaluationPeriod = new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31));
            var evaluation = new Evaluation(evaluatee, evaluationPeriod);

            // act
            void action() => new SkillGoal(null, skillEvaluator, targetLevel, currentLevel, evaluation);

            // assert
            Assert.Throws<ArgumentNullException>(action);

        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenCreatingInstanceIfSkillEvaluatorIsNull()
        {
            // arrange
            var skill = new Skill("C#");
            var targetLevel = SkillLevel.Advanced;
            var currentLevel = SkillLevel.Average;
            var evaluatee = new Employee(101, "John Doe").Evaluatee;
            var evaluationPeriod = new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31));
            var evaluation = new Evaluation(evaluatee, evaluationPeriod);

            // act
            void action() => new SkillGoal(skill, null, targetLevel, currentLevel, evaluation); ;

            // assert
            Assert.Throws<ArgumentNullException>(action);

        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenCreatingInstanceIfEvaluationIsNull()
        {
            // arrange
            var skill = new Skill("C#");
            var skillEvaluator = new Employee(100, "Jane Doe").SkillEvaluator;
            var targetLevel = SkillLevel.Advanced;
            var currentLevel = SkillLevel.Average;
            var evaluatee = new Employee(101, "John Doe").Evaluatee;
            var evaluationPeriod = new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31));

            // act
            void action() => new SkillGoal(skill, skillEvaluator, targetLevel, currentLevel, null);

            // assert
            Assert.Throws<ArgumentNullException>(action);

        }

        [Fact]
        public void ShouldCreateInstance()
        {
            // arrange
            var skill = new Skill("C#");
            var skillEvaluator = new Employee(100, "Jane Doe").SkillEvaluator;
            var targetLevel = SkillLevel.Advanced;
            var currentLevel = SkillLevel.Average;
            var evaluatee = new Employee(101, "John Doe").Evaluatee;
            var evaluationPeriod = new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31));
            var evaluation = new Evaluation(evaluatee, evaluationPeriod);

            // act
            var result = new SkillGoal(skill, skillEvaluator, targetLevel, currentLevel, evaluation);

            // assert
            Assert.Equal(skillEvaluator, result.SkillEvaluator);
            Assert.Equal(evaluatee, result.Evaluatee);
            Assert.Equal(evaluationPeriod, result.EvaluationPeriod);
            Assert.Equal(evaluation, result.Evaluation);
            Assert.Equal(currentLevel, result.InitialSkillLevel.PersonalSkill.SkillLevel);
            Assert.Equal(targetLevel, result.TargetSkillLevel);
            Assert.NotNull(result.SkillScore);
        }
    }
}
