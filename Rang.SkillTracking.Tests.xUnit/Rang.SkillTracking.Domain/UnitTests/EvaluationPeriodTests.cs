using Rang.SkillTracking.Domain.Skills;
using System;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.Rang.SkillTracking.Domain.UnitTests
{
    public class EvaluationPeriodTests
    {
        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenCreatingInstanceIfTimeZoneIsNull()
        {
            // arrange

            // act
            void action() => new EvaluationPeriod(null, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31));

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldThrowApplicationExceptionWhenCreatingInstanceIfEndDateisBeforeStartDate()
        {
            // arrange

            // act
            void action() => new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 12, 31), new DateTime(DateTime.Today.Year, 1, 1));

            // assert
            Assert.Throws<ApplicationException>(action);
        }

        [Fact]
        public void ShouldCreateInstance()
        {
            // arrange

            // act
            var result = new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31));

            // assert
            Assert.Equal(TimeZoneInfo.Local, result.TimeZoneInfo);
            Assert.Equal(new DateTime(DateTime.Today.Year, 1, 1), result.StartDate);
            Assert.Equal(new DateTime(DateTime.Today.Year, 12, 31), result.EndDate);
        }
    }
}
