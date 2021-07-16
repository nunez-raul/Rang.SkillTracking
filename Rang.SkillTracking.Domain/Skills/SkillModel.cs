using Rang.SkillTracking.Domain.Common;
using System;

namespace Rang.SkillTracking.Domain.Skills
{
    public class SkillModel : BaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
