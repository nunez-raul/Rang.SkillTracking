using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rang.SkillTracking.Domain.Skills;

namespace Rang.SkillTracking.Persistence.Ef.TypeConfig
{
    internal class EvaluationModelConfiguration : IEntityTypeConfiguration<EvaluationModel>
    {
        public void Configure(EntityTypeBuilder<EvaluationModel> builder)
        {
            builder.ToTable("Evaluations").HasKey(model => model.Number);

            builder.HasOne(evaluation => evaluation.EvaluateeModel).WithMany(evaluatee => evaluatee.EvaluationModels).HasForeignKey(evaluation => evaluation.EvaluateeNumber);
            builder.HasOne(evaluation => evaluation.EvaluationPeriodModel).WithOne().HasForeignKey<EvaluationModel>(evaluation => evaluation.EvaluationPeriodNumber);
        }
    }
}
