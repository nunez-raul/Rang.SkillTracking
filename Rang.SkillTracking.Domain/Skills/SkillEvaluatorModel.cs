using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using System.Collections.Generic;

namespace Rang.SkillTracking.Domain.Skills
{
    public class SkillEvaluatorModel: BaseModel
    {
        public EmployeeModel EmployeeModel { get; set; }
        public List<SkillGoalModel> SkillGoalModels { get; set; }
        public List<TrackingPointModel> TrackingPointModels { get; set; }
    }
}
