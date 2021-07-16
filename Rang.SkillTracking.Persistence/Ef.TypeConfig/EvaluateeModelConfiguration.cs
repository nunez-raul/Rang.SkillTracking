using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rang.SkillTracking.Domain.Skills;

namespace Rang.SkillTracking.Persistence.Ef.TypeConfig
{
    internal class EvaluateeModelConfiguration : IEntityTypeConfiguration<EvaluateeModel>
    {
        public void Configure(EntityTypeBuilder<EvaluateeModel> builder)
        {
            builder.ToTable("Evaluatees").HasKey(model => model.Number);
            builder.Property(e => e.EmployeeNumber).IsRequired();

            builder.HasOne(evaluatee => evaluatee.EmployeeModel).WithOne(employee => employee.EvaluateeModel).HasForeignKey<EvaluateeModel>(evaluatee => evaluatee.EmployeeNumber);
            builder.HasMany(evaluatee => evaluatee.EvaluationModels).WithOne(evaluation => evaluation.EvaluateeModel).HasForeignKey(evaluation => evaluation.EvaluateeNumber);
        }
    }
}
