using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rang.SkillTracking.Domain.Skills;

namespace Rang.SkillTracking.Persistence.Ef.TypeConfig
{
    public class EvaluationPeriodModelConfiguration : IEntityTypeConfiguration<EvaluationPeriodModel>
    {
        public void Configure(EntityTypeBuilder<EvaluationPeriodModel> builder)
        {
            builder.ToTable("EvaluationPeriod").HasKey(model => model.Number);

            builder.Ignore(model => model.TimeZoneInfo);
            builder.Ignore(model => model.StartDate);
            builder.Ignore(model => model.EndDate);
            builder.Property(model => model.StartDateInUtc).IsRequired();
            builder.Property(model => model.EndDateInUtc).IsRequired();
        }
    }
}
