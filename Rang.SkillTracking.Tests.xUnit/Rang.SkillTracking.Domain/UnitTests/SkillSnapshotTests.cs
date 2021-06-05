using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using Rang.SkillTracking.Domain.Skills;
using System;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.Rang.SkillTracking.Domain.UnitTests
{
    public class SkillSnapshotTests
    {
        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenCreatingInstanceIfTrackingPointIsNull()
        {
            // arrange
            var skill = new Skill("C#");
            var currentLevel = SkillLevel.Average;
            var personalProfile = new Employee(101, "John Doe").Profile;
            var personalSkill = new PersonalSkill(skill, currentLevel, personalProfile);

            // act
            void action() => new SkillSnapshot(null, personalSkill);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenCreatingInstanceIfPersonalSkillIsNull()
        {
            // arrange
            var skillEvaluator = new Employee(100, "Jane Doe").SkillEvaluator;
            var evaluationPeriod = new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31));
            var trackingPoint = new TrackingPoint(skillEvaluator, evaluationPeriod, DateTime.Today);

            // act
            void action() => new SkillSnapshot(trackingPoint, null);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldCreateInstance()
        {
            // arrange
            var skill = new Skill("C#");
            var currentLevel = SkillLevel.Average;
            var personalProfile = new Employee(101, "John Doe").Profile;
            var personalSkill = new PersonalSkill(skill, currentLevel, personalProfile);
            var skillEvaluator = new Employee(100, "Jane Doe").SkillEvaluator;
            var evaluationPeriod = new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31));
            var trackingPoint = new TrackingPoint(skillEvaluator, evaluationPeriod, DateTime.Today);

            // act
            var result = new SkillSnapshot(trackingPoint, personalSkill);

            // assert
            Assert.Equal(trackingPoint, result.TrackingPoint);
            Assert.Equal(personalSkill, result.PersonalSkill);
        }
    }
}
