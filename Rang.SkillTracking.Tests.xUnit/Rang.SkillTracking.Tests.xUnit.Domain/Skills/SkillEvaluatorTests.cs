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
        public void ShouldSetSkillScore()
        {
            // arrange
            var evaluationPeriod = new EvaluationPeriod();
            var skillEvaluator = new Employee().SkillEvaluator;
            var skill = new Skill("C#");
            var evaluatee = new Employee().Evaluatee;
            var evaluation = new Evaluation(evaluatee, evaluationPeriod);
            new SkillGoal(skill, skillEvaluator, SkillLevel.Expert, SkillLevel.Advanced, evaluation);

            // act
            var result = skillEvaluator.SetSkillScore(evaluatee, skill, evaluationPeriod, SkillLevel.Advanced, 10, "Excelsior!!!");

            // assert
            Assert.Equal(OperationStatusCode.Success, result);
            Assert.Equal(10, skillEvaluator.SkillGoals.First().SkillScore.Score);
        }

        [Fact]
        public void ShouldAddNewTrackingPoint()
        {
            // arrange
            var skillEvaluator = new Employee().SkillEvaluator;

            // act
            var result = skillEvaluator.AddNewTrackingPoint(new EvaluationPeriod(), DateTime.Today);

            // assert
            Assert.Equal(OperationStatusCode.Success, result);
            Assert.Single(skillEvaluator.TrackingPoints);
        }

        [Fact]
        public void ShouldAddNewSkillSnapshot()
        {
            // arrange
            var skillEvaluator = new Employee().SkillEvaluator;
            var skill = new Skill("C#");
            var evaluatee = new Employee().Evaluatee;
            skillEvaluator.AddNewTrackingPoint(new EvaluationPeriod(), DateTime.Today);

            // act
            var result = skillEvaluator.AddNewSkillSnapshot(evaluatee, skill, SkillLevel.Noob, DateTime.Today);

            // assert
            Assert.Equal(OperationStatusCode.Success, result);
            Assert.Single(skillEvaluator.TrackingPoints.First().SkillSnapshots);
        }
    }
}
