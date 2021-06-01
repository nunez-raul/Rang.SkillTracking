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
        public void ShouldAddNewSkillGoal()
        {
            // arrange
            var skillEvaluator = new Employee().SkillEvaluator;
            var skill = new Skill("C#");
            var evaluatee = new Employee().Evaluatee;
            var evaluationPeriod = new EvaluationPeriod();
            var evaluation = new Evaluation(evaluatee, evaluationPeriod);
            var skillGoal = new SkillGoal(skill, skillEvaluator, SkillLevel.Expert, SkillLevel.Advanced, evaluation);

            // act
            var result = skillEvaluator.AddNewSkillGoal(skillGoal);

            // assert
            Assert.Equal(OperationStatusCode.Success, result);
            Assert.Single(skillEvaluator.SkillGoals);
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
        public void ShouldSetSkillScore()
        {
            // arrange
            var skillEvaluator = new Employee().SkillEvaluator;
            
            var evaluatee = new Employee().Evaluatee;
            var evaluationPeriod = new EvaluationPeriod();
            evaluatee.AddNewEvaluation(evaluationPeriod);
            var evaluation = evaluatee.Evaluations.First();

            var skill = new Skill("C#");
            var skillGoal = new SkillGoal(skill, skillEvaluator, SkillLevel.Expert, SkillLevel.Advanced, evaluation);
            evaluation.AddNewSkillGoal(skillGoal);
            skillEvaluator.AddNewSkillGoal(skillGoal);

            // act
            var result = skillEvaluator.SetSkillScore(evaluatee, skill, evaluationPeriod, SkillLevel.Advanced, 10, "Excelsior!!!");

            // assert
            Assert.Equal(OperationStatusCode.Success, result);
            Assert.Equal(10, skillEvaluator.SkillGoals.First().SkillScore.Score);
        }
    }
}
