using Rang.SkillTracking.Domain.Employees;
using Rang.SkillTracking.Domain.Skills;
using System;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.UnitTests
{
    public class TrackingPointTests
    {
        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenCreatingInstanceIfEvaluatorIsNull()
        {
            // arrange
            var evaluationPeriod = new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31));
            
            // act
            void action() => new TrackingPoint(null, evaluationPeriod, DateTime.Today);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenCreatingInstanceIfPeriodIsNull()
        {
            // arrange
            var skillEvaluator = new Employee(100, "Jane Doe").SkillEvaluator;
            
            // act
            void action() => new TrackingPoint(skillEvaluator, null, DateTime.Today);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldCreateInstance()
        {
            // arrange
            var skillEvaluator = new Employee(100, "Jane Doe").SkillEvaluator;
            var evaluationPeriod = new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31));

            // act
            var result = new TrackingPoint(skillEvaluator, evaluationPeriod, DateTime.Today);

            // assert
            Assert.Equal(skillEvaluator, result.Owner);
            Assert.Equal(evaluationPeriod, result.EvaluationPeriod);
            Assert.Equal(DateTime.Today, result.Date);
            Assert.NotNull(result.SkillSnapshots);
        }
    }
}
