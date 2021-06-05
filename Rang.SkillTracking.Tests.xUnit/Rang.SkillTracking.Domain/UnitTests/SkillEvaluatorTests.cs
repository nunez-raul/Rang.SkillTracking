using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using Rang.SkillTracking.Domain.Skills;
using System;
using System.Linq;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.Rang.SkillTracking.Domain.UnitTests
{
    public class SkillEvaluatorTests
    {
        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenCreatingInstanceIfEmployeeIsNull()
        {
            // arrange

            // act
            void action() => new SkillEvaluator(null);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldCreateInstance()
        {
            // arrange
            var employee = new Employee(101, "John Doe");

            // act
            var result = new SkillEvaluator(employee);

            // assert
            Assert.Equal(employee, result.Employee);
            Assert.NotNull(result.SkillGoals);
            Assert.NotNull(result.TrackingPoints);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenAddNewSkillGoalIfSkillIsNull()
        {
            // arrange
            var skillEvaluator = new Employee(100, "Jabe Doe").SkillEvaluator;
            var evaluatee = new Employee(101, "John Doe").Evaluatee;
            var evaluationPeriod = new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31));
            var evaluation = new Evaluation(evaluatee, evaluationPeriod);
            var targetLevel = SkillLevel.Advanced;
            var currentLevel = SkillLevel.Average;

            // act
            void action() => skillEvaluator.AddNewSkillGoal(null, skillEvaluator, targetLevel, currentLevel, evaluation);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldAddNewSkillGoal()
        {
            // arrange
            var skillEvaluator = new Employee(100, "Jane Doe").SkillEvaluator;
            var skill = new Skill("C#");
            var evaluatee = new Employee(101, "John Doe").Evaluatee;
            var evaluationPeriod = new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31));
            var evaluation = new Evaluation(evaluatee, evaluationPeriod);
            var targetLevel = SkillLevel.Advanced;
            var currentLevel = SkillLevel.Average;

            // act
            var result = skillEvaluator.AddNewSkillGoal(skill, skillEvaluator, targetLevel, currentLevel, evaluation);

            // assert
            Assert.Equal(OperationStatusCode.Success, result.OperationStatusCode);
            Assert.Single(skillEvaluator.SkillGoals);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenAddNewTrackingPointIfPeriodIsNull()
        {
            // arrange
            var skillEvaluator = new Employee(100, "Jane Doe").SkillEvaluator;

            // act
            void action() => skillEvaluator.AddNewTrackingPoint(null, DateTime.Today);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldAddNewTrackingPoint()
        {
            // arrange
            var skillEvaluator = new Employee(100, "Jane Doe").SkillEvaluator;
            var evaluationPeriod = new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31));

            // act
            var result = skillEvaluator.AddNewTrackingPoint(evaluationPeriod, DateTime.Today);

            // assert
            Assert.Equal(OperationStatusCode.Success, result.OperationStatusCode);
            Assert.Single(skillEvaluator.TrackingPoints);
        }

        [Fact]
        public void ShouldAddNewSkillSnapshotToTrackingPoint()
        {
            // arrange
            var skillEvaluator = new Employee(100, "Jane Doe").SkillEvaluator;
            var skill = new Skill("C#");
            var evaluatee = new Employee(101, "John Doe").Evaluatee;
            skillEvaluator.AddNewTrackingPoint(new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31)), DateTime.Today);

            // act
            var result = skillEvaluator.AddNewSkillSnapshotToTrackingPoint(evaluatee, skill, SkillLevel.Noob, DateTime.Today);

            // assert
            Assert.Equal(OperationStatusCode.Success, result.OperationStatusCode);
            Assert.Single(skillEvaluator.TrackingPoints.First().SkillSnapshots);
        }

        [Fact]
        public void ShouldReturnMissingTrackingPointWhenAddNewSkillSnapshotToNotExistingTrackingPoint()
        {
            // arrange
            var skillEvaluator = new Employee(100, "Jane Doe").SkillEvaluator;
            var skill = new Skill("C#");
            var evaluatee = new Employee(101, "John Doe").Evaluatee;

            // act
            var result = skillEvaluator.AddNewSkillSnapshotToTrackingPoint(evaluatee, skill, SkillLevel.Noob, DateTime.Today);

            // assert
            Assert.Equal(OperationStatusCode.MissingTrackingPoint, result.OperationStatusCode);
            Assert.Empty(skillEvaluator.TrackingPoints);
        }


        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenSetSkillScoreToSkillGoalIfGoalIsNull()
        {
            // arrange
            var skillEvaluator = new Employee(100, "Jane Doe").SkillEvaluator;

            // act
            var skillLevelAchieved = SkillLevel.Advanced;
            void action() => skillEvaluator.SetSkillScoreToSkillGoal(null, skillLevelAchieved, 10, "Excelsior!!!");

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldReturnMissingGoalWhenSetSkillScoreToSkillGoalIfGoalIsNotFound()
        {
            // arrange
            var skillEvaluator = new Employee(100, "Jane Doe").SkillEvaluator;
            var skill = new Skill("C#");
            var evaluatee = new Employee(101, "John Doe").Evaluatee;
            var evaluationPeriod = new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31));
            var evaluation = new Evaluation(evaluatee, evaluationPeriod);
            var targetLevel = SkillLevel.Advanced;
            var currentLevel = SkillLevel.Average;
            var skillGoal = new SkillGoal(skill, skillEvaluator, targetLevel, currentLevel, evaluation);

            // act
            var skillLevelAchieved = SkillLevel.Advanced;
            var result = skillEvaluator.SetSkillScoreToSkillGoal(skillGoal, skillLevelAchieved, 10, "Excelsior!!!");

            // assert
            Assert.Equal(OperationStatusCode.MissingSkillGoal, result.OperationStatusCode);
            Assert.Empty(skillEvaluator.SkillGoals);
        }

        [Fact]
        public void ShouldSetSkillScoreToExistingSkillGoal()
        {
            // arrange
            var skillEvaluator = new Employee(100, "Jane Doe").SkillEvaluator;
            var skill = new Skill("C#");
            var evaluatee = new Employee(101, "John Doe").Evaluatee;
            var evaluationPeriod = new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31));
            var evaluation = new Evaluation(evaluatee, evaluationPeriod);
            var targetLevel = SkillLevel.Advanced;
            var currentLevel = SkillLevel.Average;
            skillEvaluator.AddNewSkillGoal(skill, skillEvaluator, targetLevel, currentLevel, evaluation);

            // act
            var skillGoal = skillEvaluator.SkillGoals.First();
            var skillLevelAchieved = SkillLevel.Advanced;
            var result = skillEvaluator.SetSkillScoreToSkillGoal(skillGoal, skillLevelAchieved, 10, "Excelsior!!!");

            // assert
            Assert.Equal(OperationStatusCode.Success, result.OperationStatusCode);
            Assert.Equal(10, skillGoal.SkillScore.Score);
            Assert.Equal(currentLevel, skillGoal.InitialSkillLevel.PersonalSkill.SkillLevel);
            Assert.Equal(skillLevelAchieved, skillGoal.SkillScore.AchievedSkillLevel.PersonalSkill.SkillLevel);
        }
    }
}
