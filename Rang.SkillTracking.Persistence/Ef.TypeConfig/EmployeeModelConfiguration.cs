using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rang.SkillTracking.Domain.Employees;

namespace Rang.SkillTracking.Persistence.Ef.TypeConfig
{
    public class EmployeeModelConfiguration : IEntityTypeConfiguration<EmployeeModel>
    {
        public void Configure(EntityTypeBuilder<EmployeeModel> builder)
        {
            builder.ToTable("Employees").HasKey(model => model.Number);

            builder.Property(model => model.Number).IsRequired();
            builder.Property(model => model.Name).IsRequired();
            builder.Property(model => model.IsActive).IsRequired();

            builder.HasOne(employee => employee.EvaluateeModel).WithOne(evaluatee => evaluatee.EmployeeModel);
        }
    }
}
