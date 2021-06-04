using Rang.SkillTracking.Domain.Employees;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.Rang.SkillTracking.Domain.UnitTests
{
    public class EmployeeTests
    {
        [Fact]
        public void ShouldCreateInstance()
        {
            // arrange

            // act
            var result = new Employee();

            // assert
            Assert.NotNull(result);
            Assert.NotNull(result.Profile);
            Assert.NotNull(result.Evaluatee);
            Assert.NotNull(result.SkillEvaluator);
        }
    }
}
