using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rang.SkillTracking.Domain.Skills;

namespace Rang.SkillTracking.Persistence.Ef.TypeConfig
{
    public class SkillGoalConfiguration: IEntityTypeConfiguration<SkillGoalModel>
    {
        public void Configure(EntityTypeBuilder<SkillGoalModel> builder)
        {
            builder.ToTable("SkillGoals").HasKey(model => model.Id);

            builder.HasOne(skillGoal => skillGoal.EvaluationModel).WithMany(evaluation => evaluation.SkillGoalModels).HasForeignKey(skillGoal => skillGoal.EvaluationNumber);
        }
    }
}
