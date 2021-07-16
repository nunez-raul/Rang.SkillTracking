using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Skills;
using System.Collections.Generic;

namespace Rang.SkillTracking.Domain.Employees
{
    public class PersonalProfileModel : BaseModel
    {
        public IEnumerable<PersonalSkillModel> SkillModels { get; set; }
        public EmployeeModel EmployeeModel { get; set; }
    }
}
