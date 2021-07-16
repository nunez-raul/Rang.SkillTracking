using Rang.SkillTracking.Domain.Common;

namespace Rang.SkillTracking.Domain.Skills
{
    public class SkillGoalModel: BaseModel
    {
        public new uint Id { get; set; }

        public SkillSnapshotModel InitialSkillLevelModel { get; set; }
        public SkillLevel TargetSkillLevel { get; set; }

        public uint EvaluationNumber { get; set; }
        public EvaluationModel EvaluationModel { get; set; }

        public EvaluationPeriodModel EvaluationPeriodModel { get; set; }
        public EvaluateeModel EvaluateeModel { get; set; }
        public SkillEvaluatorModel SkillEvaluatorModel { get; set; }
        public SkillScoreModel SkillScoreModel { get; set; }
    }
}
