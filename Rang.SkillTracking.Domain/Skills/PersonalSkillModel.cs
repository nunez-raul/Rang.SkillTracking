using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;

namespace Rang.SkillTracking.Domain.Skills
{
    public class PersonalSkillModel: BaseModel
    {
        public SkillModel SkillModel { get;  set; }
        public PersonalProfileModel ProfileModel { get;  set; }
        public SkillLevel SkillLevel { get;  set; }
    }
}
