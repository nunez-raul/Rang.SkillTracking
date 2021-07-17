using Rang.SkillTracking.Domain.Employees;
using System;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.UnitTests
{
    public class EmployeeTests
    {
        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenCreatingInstanceIfNameisNullOrEmpty()
        {
            // arrange

            // act
            void action() => new Employee( 101, null);

            // assert
            Assert.Throws<ArgumentNullException>(action);

        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenCreatingInstanceIfModelIsNull()
        {
            // arrange
            EmployeeModel employeeModel = null;

            // act
            void action() => new Employee(employeeModel);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldCreateInstance()
        {
            // arrange

            // act
            var result = new Employee(101, "John Doe");

            // assert
            Assert.Equal("John Doe", result.Name);
            Assert.Equal("101", result.Number.ToString());
            Assert.NotNull(result.Profile);
            Assert.NotNull(result.Evaluatee);
            Assert.NotNull(result.SkillEvaluator);
        }

        [Fact]
        public void ShouldReturnModelWhenGetModel()
        {
            // arrange
            var employee = new Employee(101, "John Doe");

            // act
            var result = employee.GetModel();

            // assert
            Assert.Equal("John Doe", result.Name);
            Assert.Equal("101", result.Number.ToString());
            //Assert.NotNull(result.Profile);
            Assert.NotNull(result.EvaluateeModel);
            //Assert.NotNull(result.SkillEvaluator);
        }
    }
}
