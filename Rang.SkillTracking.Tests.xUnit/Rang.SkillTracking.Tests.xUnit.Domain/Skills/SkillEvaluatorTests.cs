using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using Rang.SkillTracking.Domain.Skills;
using System;
using System.Linq;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.Rang.SkillTracking.Tests.xUnit.Domain.Skills
{
    public class SkillEvaluatorTests
    {
        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenAddNewSkillGoalIfGoalIsNull()
        {
            // arrange
            var skillEvaluator = new Employee().SkillEvaluator;

            // act
            void action() => skillEvaluator.AddNewSkillGoal(null);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldAddNewSkillGoal()
        {
            // arrange
            var skillEvaluator = new Employee().SkillEvaluator;
            var skill = new Skill("C#");
            var evaluatee = new Employee().Evaluatee;
            var evaluationPeriod = new EvaluationPeriod();
            var evaluation = new Evaluation(evaluatee, evaluationPeriod);
            var targetLevel = SkillLevel.Advanced;
            var currentLevel = SkillLevel.Average;
            var skillGoal = new SkillGoal(skill, skillEvaluator, targetLevel, currentLevel, evaluation);

            // act
            var result = skillEvaluator.AddNewSkillGoal(skillGoal);

            // assert
            Assert.Equal(OperationStatusCode.Success, result);
            Assert.Single(skillEvaluator.SkillGoals);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenAddNewTrackingPointIfPeriodIsNull()
        {
            // arrange
            var skillEvaluator = new Employee().SkillEvaluator;

            // act
            void action() => skillEvaluator.AddNewTrackingPoint(null, DateTime.Today);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldAddNewTrackingPoint()
        {
            // arrange
            var skillEvaluator = new Employee().SkillEvaluator;
            var evaluationPeriod = new EvaluationPeriod();

            // act
            var result = skillEvaluator.AddNewTrackingPoint(evaluationPeriod, DateTime.Today);

            // assert
            Assert.Equal(OperationStatusCode.Success, result);
            Assert.Single(skillEvaluator.TrackingPoints);
        }

        [Fact]
        public void ShouldAddNewSkillSnapshotToTrackingPoint()
        {
            // arrange
            var skillEvaluator = new Employee().SkillEvaluator;
            var skill = new Skill("C#");
            var evaluatee = new Employee().Evaluatee;
            skillEvaluator.AddNewTrackingPoint(new EvaluationPeriod(), DateTime.Today);

            // act
            var result = skillEvaluator.AddNewSkillSnapshotToTrackingPoint(evaluatee, skill, SkillLevel.Noob, DateTime.Today);

            // assert
            Assert.Equal(OperationStatusCode.Success, result);
            Assert.Single(skillEvaluator.TrackingPoints.First().SkillSnapshots);
        }

        [Fact]
        public void ShouldReturnMissingTrackingPointWhenAddNewSkillSnapshotToNotExistingTrackingPoint()
        {
            // arrange
            var skillEvaluator = new Employee().SkillEvaluator;
            var skill = new Skill("C#");
            var evaluatee = new Employee().Evaluatee;

            // act
            var result = skillEvaluator.AddNewSkillSnapshotToTrackingPoint(evaluatee, skill, SkillLevel.Noob, DateTime.Today);

            // assert
            Assert.Equal(OperationStatusCode.MissingTrackingPoint, result);
            Assert.Empty(skillEvaluator.TrackingPoints);
        }


        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenSetSkillScoreToSkillGoalIfGoalIsNull()
        {
            // arrange
            var skillEvaluator = new Employee().SkillEvaluator;

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
            var skillEvaluator = new Employee().SkillEvaluator;
            var skill = new Skill("C#");
            var evaluatee = new Employee().Evaluatee;
            var evaluationPeriod = new EvaluationPeriod();
            var evaluation = new Evaluation(evaluatee, evaluationPeriod);
            var targetLevel = SkillLevel.Advanced;
            var currentLevel = SkillLevel.Average;
            var skillGoal = new SkillGoal(skill, skillEvaluator, targetLevel, currentLevel, evaluation);

            // act
            var skillLevelAchieved = SkillLevel.Advanced;
            var result = skillEvaluator.SetSkillScoreToSkillGoal(skillGoal, skillLevelAchieved, 10, "Excelsior!!!");

            // assert
            Assert.Equal(OperationStatusCode.MissingSkillGoal, result);
            Assert.Empty(skillEvaluator.SkillGoals);
        }

        [Fact]
        public void ShouldSetSkillScoreToExistingSkillGoal()
        {
            // arrange
            var skillEvaluator = new Employee().SkillEvaluator;
            var skill = new Skill("C#");
            var evaluatee = new Employee().Evaluatee;
            var evaluationPeriod = new EvaluationPeriod();
            var evaluation = new Evaluation(evaluatee, evaluationPeriod);
            var targetLevel = SkillLevel.Advanced;
            var currentLevel = SkillLevel.Average;
            var skillGoal = new SkillGoal(skill, skillEvaluator, targetLevel, currentLevel, evaluation);
            skillEvaluator.AddNewSkillGoal(skillGoal);

            // act
            var skillLevelAchieved = SkillLevel.Advanced;
            var result = skillEvaluator.SetSkillScoreToSkillGoal(skillGoal, skillLevelAchieved, 10, "Excelsior!!!");

            // assert
            Assert.Equal(OperationStatusCode.Success, result);
            Assert.Equal(10, skillEvaluator.SkillGoals.First().SkillScore.Score);
        }
    }
}
