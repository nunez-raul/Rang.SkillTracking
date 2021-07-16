using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Skills;

namespace Rang.SkillTracking.Domain.Employees
{
    public class EmployeeModel: BaseModel
    {
        public uint Number { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public EvaluateeModel EvaluateeModel { get; set; }
    }
}
