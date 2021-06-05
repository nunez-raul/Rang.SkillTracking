using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using Rang.SkillTracking.Domain.Skills;
using System;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.UnitTests
{
    public class EvaluationTests
    {
        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenCreatingInstanceIfEvaluateeIsNull()
        {
            // arrange

            // act
            void action() => new Evaluation(null, new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31)));

            // assert
            Assert.Throws<ArgumentNullException>(action);

        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenCreatingInstanceIfPeriodIsNull()
        {
            // arrange

            // act
            void action() => new Evaluation(new Employee(101, "John Doe").Evaluatee, null);

            // assert
            Assert.Throws<ArgumentNullException>(action);

        }

        [Fact]
        public void ShouldCreateInstance()
        {
            // arrange
            var evaluatee = new Employee(101, "John Doe").Evaluatee;
            var evaluationPeriod = new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31));

            // act
            var result = new Evaluation(evaluatee, evaluationPeriod);

            // assert
            Assert.Equal(evaluatee, result.Evaluatee);
            Assert.Equal(evaluationPeriod, result.EvaluationPeriod);
            Assert.NotNull(result.SkillGoals);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenAddNewSkillGoalIfSkillIsNull()
        {
            // arrange
            var evaluation = new Evaluation(new Employee(101, "John Doe").Evaluatee, new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31)));
            var skillEvaluator = new Employee(100, "Jane Doe").SkillEvaluator;
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
            var evaluation = new Evaluation(new Employee(101, "John Doe").Evaluatee, new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31)));
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
            var evaluation = new Evaluation(new Employee(101, "John Doe").Evaluatee, new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31)));
            var skill = new Skill("C#");
            var skillEvaluator = new Employee(100, "Jane Doe").SkillEvaluator;
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
