using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using Rang.SkillTracking.Domain.Skills;
using System;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.Rang.SkillTracking.Tests.xUnit.Domain.Skills
{
    public class SkillEvaluatorTests
    {
        [Fact]
        public void ShouldSetSkillScore()
        {
            // arrange
            var skillEvaluator = new Employee().SkillEvaluator;
            var skill = new Skill("C#");
            var evaluatee = new Employee().Evaluatee;
            var skillGoal = new SkillGoal(skill, skillEvaluator, SkillLevel.Advanced, new Evaluation(evaluatee, new EvaluationPeriod()));

            // act
            var result = skillEvaluator.SetSkillScore(skillGoal,SkillLevel.Expert, 10, "Excelsior!!!");

            // assert
            Assert.Equal(OperationStatusCode.Success, result);
            Assert.Equal(10, skillGoal.SkillScore.Score);
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
            var personalSkill = new PersonalSkill(skill, new Employee().Profile);
            var trackingPoint = new TrackingPoint(skillEvaluator, new EvaluationPeriod(), DateTime.Today);

            // act
            var result = skillEvaluator.AddNewSkillSnapshot(personalSkill, trackingPoint);

            // assert
            Assert.Equal(OperationStatusCode.Success, result);
            Assert.Single(trackingPoint.SkillSnapshots);
        }
    }
}
