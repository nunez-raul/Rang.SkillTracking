using Rang.SkillTracking.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.UnitTests
{
    public class SkillTrackingSystemTests
    {
        [Fact]
        public void ShouldCreateInstance()
        {
            // arrange

            // act
            var result = new SkillTrackingSystem();

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenAddNewSkillsIfSkillNamesArrayIsNull()
        {
            // arrange
            var system = new SkillTrackingSystem();

            // act
            void action() => system.AddNewSkills(null);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldAddNewSkills()
        {
            // arrange
            var system = new SkillTrackingSystem();
            var skillNames = new string[] { "C#", "Scrum", "Vue", "React"};

            // act
            var result = system.AddNewSkills(skillNames);

            //assert
            Assert.Equal(skillNames.Length, result.Count());
        }
    }
}
