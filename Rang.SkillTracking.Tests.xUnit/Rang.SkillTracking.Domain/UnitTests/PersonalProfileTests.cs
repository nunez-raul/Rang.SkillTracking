using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using Rang.SkillTracking.Domain.Skills;
using System;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.Rang.SkillTracking.Domain.UnitTests
{
    public class PersonalProfileTests
    {
        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenCreatingInstanceIfEmployeeIsNull()
        {
            // arrange

            // act
            void action() => new PersonalProfile(null);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldCreateInstance()
        {
            // arrange
            var employee = new Employee(101, "John Doe");

            // act
            var result = new PersonalProfile(employee);

            // assert
            Assert.Equal(employee, result.Employee);
            Assert.NotNull(result.Skills);
        }

        [Fact]
        public void ShouldThrowExceptionWhenAddNewPersonalSkillifSkillIsNull()
        {
            // arrange
            var personalProfile = new Employee(101, "John Doe").Profile;
            var currentLevel = SkillLevel.Average;

            // act
            void action() => personalProfile.AddNewPersonalSkill(null, currentLevel);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldAddNewPersonalSkill()
        {
            // arrange
            var personalProfile = new Employee(101, "John Doe").Profile;
            var skill = new Skill("C#");
            var currentLevel = SkillLevel.Average;

            // act
            var result = personalProfile.AddNewPersonalSkill(skill, currentLevel);

            // assert
            Assert.Equal(OperationStatusCode.Success, result.OperationStatusCode);
            Assert.Single(personalProfile.Skills);
        }
    }
}
