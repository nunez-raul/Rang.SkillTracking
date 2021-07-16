using Rang.SkillTracking.Domain.Common;
using Rang.SkillTracking.Domain.Employees;
using System.Collections.Generic;

namespace Rang.SkillTracking.Domain.Skills
{
    public class EvaluateeModel : BaseModel
    {
        public uint Number { get; set; }

        public uint EmployeeNumber { get; set; }
        public EmployeeModel EmployeeModel { get; set; }

        public List<EvaluationModel> EvaluationModels { get; set; }

        public EvaluateeModel()
        {
            
        }

        public EvaluateeModel(EmployeeModel employeeModel)
        {
            EmployeeModel = employeeModel;
        }
    }
}
