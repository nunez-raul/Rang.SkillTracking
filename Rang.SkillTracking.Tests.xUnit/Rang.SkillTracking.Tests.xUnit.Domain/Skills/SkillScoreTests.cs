using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using Rang.SkillTracking.Domain.Skills;
using System;
using System.Linq;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.Rang.SkillTracking.Tests.xUnit.Domain.Skills
{
    public class SkillScoreTests
    {
        [Fact]
        public void ShouldSetScore() 
        {
            // arrange
            var skill = new Skill("C#");
            var skillEvaluator = new Employee().SkillEvaluator;
            var targetLevel = SkillLevel.Advanced;
            var currentLevel = SkillLevel.Average;
            var evaluatee = new Employee().Evaluatee;
            var evaluationPeriod = new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31));
            var evaluation = new Evaluation(evaluatee, evaluationPeriod);
            var skillGoal = new SkillGoal(skill, skillEvaluator, targetLevel, currentLevel, evaluation);
            var skillScore = new SkillScore(skillGoal);
            skillEvaluator.AddNewSkillGoal(skill, skillEvaluator, targetLevel, currentLevel, evaluation);

            // act
            var skillLevelAchieved = SkillLevel.Advanced;
            skillScore.SetScore(skillLevelAchieved, 10, "Excelsior!!!");

            // assert
            Assert.Equal(10, skillScore.Score);
            Assert.Equal(currentLevel, skillEvaluator.SkillGoals.First().InitialSkillLevel.PersonalSkill.SkillLevel);
            Assert.Equal(skillLevelAchieved, skillScore.AchievedSkillLevel.PersonalSkill.SkillLevel);
        }
    }
}
