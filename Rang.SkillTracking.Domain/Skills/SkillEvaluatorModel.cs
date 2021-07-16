using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using System;
using System.Collections.Generic;

namespace Rang.SkillTracking.Domain.Skills
{
    public class SkillEvaluatorModel: BaseModel
    {
        public Guid Id { get; set; }
        public EmployeeModel EmployeeModel { get; set; }
        public List<SkillGoalModel> SkillGoalModels { get; set; }
        public List<TrackingPointModel> TrackingPointModels { get; set; }
    }
}
