using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Skills;
using System;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.Rang.SkillTracking.Domain.UnitTests
{
    public class SkillTests
    {
        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenCreatingInstanceIfNameisNullOrEmpty()
        {
            // arrange

            // act
            void action() => new Skill(null);

            // assert
            Assert.Throws<ArgumentNullException>(action);

        }

        [Fact]
        public void ShouldCreateInstance()
        {
            // arrange

            // act
            var result = new Skill("C#");

            // assert
            Assert.Equal("C#", result.Name);
            Assert.NotNull(result.Tags);
        }

        [Fact]
        public void ThrowArgumentNullExceptionWhenAddNewTagIfTextIsNullOrEmpty()
        {
            // arrange
            var skill = new Skill("C#");
            
            // act
            void action() => skill.AddNewTag(string.Empty);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldAddNewTag()
        {
            // arrange
            var skill = new Skill("C#");
            
            // act
            var result = skill.AddNewTag(".Net");

            // assert
            Assert.Equal(".Net", result.Text);
        }

        [Fact]
        public void ShouldAddExistingTag()
        {
            // arrange
            var skill = new Skill("C#");
            var tag = new Tag(".Net");

            // act
            skill.AddTag(tag);

            // assert
            Assert.Single(skill.Tags);
        }
    }
}
