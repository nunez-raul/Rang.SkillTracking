using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Skills;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.Rang.SkillTracking.Tests.xUnit.Domain.Skills
{
    public class EvaluationTests
    {
        [Fact]
        public void ShouldAddNewSkillGoal()
        {
            // arrange
            var evaluation = new Evaluation(new Evaluatee(), new EvaluationPeriod());
            var skillEvaluator = new SkillEvaluator();

            // act
            var result = evaluation.AddNewSkillGoal(skillEvaluator);

            // assert
            Assert.Equal(OperationStatusCode.Success, result);
            Assert.Single(evaluation.SkillGoals);
        }
    }
}
