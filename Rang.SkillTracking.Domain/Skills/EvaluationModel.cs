using Rang.SkillTracking.Domain.Common;
using System.Collections.Generic;

namespace Rang.SkillTracking.Domain.Skills
{
    public class EvaluationModel: BaseModel
    {
        public uint Number { get; set; }

        public uint EvaluateeNumber { get; set; }
        public EvaluateeModel EvaluateeModel { get;  set; }

        public uint EvaluationPeriodNumber { get; set; }
        public EvaluationPeriodModel EvaluationPeriodModel { get;  set; }

        public List<SkillGoalModel> SkillGoalModels { get; set; }
    }
}
