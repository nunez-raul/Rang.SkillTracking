using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using Rang.SkillTracking.Domain.Skills;
using System;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.Rang.SkillTracking.Domain.UnitTests
{
    public class EvaluateeTests
    {
        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenAddNewEvaluationIfEvaluationIsNull()
        {
            // arrange
            var evaluatee = new Employee().Evaluatee;

            // act
            void action() => evaluatee.AddNewEvaluation(null);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldAddNewEvaluation()
        {
            // arrange
            var evaluatee = new Employee().Evaluatee;
            var evaluationPeriod = new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31));

            // act
            var result = evaluatee.AddNewEvaluation(evaluationPeriod);

            // assert
            Assert.Equal(OperationStatusCode.Success, result.OperationStatusCode);
            Assert.Single(evaluatee.Evaluations);
        }
    }
}
