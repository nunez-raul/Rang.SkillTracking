using Microsoft.EntityFrameworkCore;
using Rang.SkillTracking.Domain.Employees;
using Rang.SkillTracking.Domain.Skills;
using Rang.SkillTracking.Persistence.Ef.TypeConfig;

namespace Rang.SkillTracking.Persistence.Ef.Context
{
    public class SkillTrackingDbContext: DbContext
    {
        // dbSets
        public virtual DbSet<EmployeeModel> EmployeeModelSet { get; set; }
        public virtual DbSet<EvaluateeModel> EvaluateeModelSet { get; set; }
        public virtual DbSet<EvaluationPeriodModel> EvaluationPeriodInUctModelSet { get; set; }

        // constructors
        public SkillTrackingDbContext(DbContextOptions<SkillTrackingDbContext> options)
            : base(options)
        {
            
        }

        // methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EvaluationPeriodModelConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeModelConfiguration());

            modelBuilder.ApplyConfiguration(new EvaluateeModelConfiguration());
            modelBuilder.ApplyConfiguration(new EvaluationModelConfiguration());
            modelBuilder.ApplyConfiguration(new SkillGoalConfiguration());
            modelBuilder.ApplyConfiguration(new SkillScoreConfiguration());
        }
    }
}
