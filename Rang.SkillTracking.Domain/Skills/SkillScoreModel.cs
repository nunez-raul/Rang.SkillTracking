using Rang.SkillTracking.Domain.Common;
using System.Collections.Generic;

namespace Rang.SkillTracking.Domain.Skills
{
    public class SkillScoreModel:BaseModel
    {
        public new uint Id { get; set; }

        public int Score { get; set; }
        //public List<string> Notes { get; set; }

        public uint SkillGoalId { get; set; }
        public SkillGoalModel SkillGoalModel { get; set; }

        public SkillSnapshotModel AchievedSkillLevelModel { get; set; }
    }
}
