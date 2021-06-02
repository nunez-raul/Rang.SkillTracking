﻿using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using Rang.SkillTracking.Domain.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.Rang.SkillTracking.Tests.xUnit.Domain.Employees
{
    public class PersonalProfileTests
    {
        [Fact]
        public void ShouldThrowExceptionWhenAddNewPersonalSkillifSkillIsNull()
        {
            // arrange
            var personalProfile = new Employee().Profile;
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
            var personalProfile = new Employee().Profile;
            var skill = new Skill("C#");
            var currentLevel = SkillLevel.Average;

            // act
            var result = personalProfile.AddNewPersonalSkill(skill, currentLevel);

            // assert
            Assert.Equal(OperationStatusCode.Success, result);
            Assert.Single(personalProfile.Skills);
        }
    }
}
