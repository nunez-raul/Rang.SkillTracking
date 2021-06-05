using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Skills;
using System.Linq;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.UnitTests
{
    public class EntityOperationResultTests
    {
        [Fact]
        public void ShouldCreateInstanceOfEntityOperationResult()
        {
            // arrange


            // act
            var result = new EntityOperationResult<Skill>(OperationStatusCode.Success);

            // assert
            Assert.Equal(OperationStatusCode.Success, result.OperationStatusCode);
        }

        [Fact]
        public void ShouldCreateInstanceOfEntityOperationResultWithEntityCollection()
        {
            // arrange
            var skillArray = new Skill[] {new ("C#") };

            // act
            var result = new EntityOperationResult<Skill>(OperationStatusCode.Success, skillArray);

            // assert
            Assert.Equal(OperationStatusCode.Success, result.OperationStatusCode);
            Assert.Equal(1, result.EntityCollectionCount);
            Assert.Equal(skillArray.First(), result.EntityCollection.First());
        }

        [Fact]
        public void ShouldCreateInstanceOfEntityOperationResultWithSingleEntity()
        {
            // arrange
            var skill = new Skill("C#");

            // act
            var result = new EntityOperationResult<Skill>(OperationStatusCode.Success, skill);

            // assert
            Assert.Equal(OperationStatusCode.Success, result.OperationStatusCode);
            Assert.Equal(1, result.EntityCollectionCount);
            Assert.Equal(skill, result.EntityCollection.First());
        }
    }
}
