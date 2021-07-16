using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rang.SkillTracking.Domain.Skills;

namespace Rang.SkillTracking.Persistence.Ef.TypeConfig
{
    public class SkillScoreConfiguration : IEntityTypeConfiguration<SkillScoreModel>
    {
        public void Configure(EntityTypeBuilder<SkillScoreModel> builder)
        {
            builder.ToTable("SkillScores").HasKey(model => model.Id);

            builder.HasOne(skillScore => skillScore.SkillGoalModel).WithOne(skillGoal => skillGoal.SkillScoreModel).HasForeignKey<SkillScoreModel>(skillScore => skillScore.SkillGoalId);
        }
    }
}
