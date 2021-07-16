using Rang.SkillTracking.Domain.Common;
using System;

namespace Rang.SkillTracking.Domain.Skills
{
    public class EvaluationPeriodModel: BaseModel
    {
        public uint Number { get; set; }
        public TimeZoneInfo TimeZoneInfo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime StartDateInUtc { get; set; }
        public DateTime EndDateInUtc { get; set; }
    }
}
