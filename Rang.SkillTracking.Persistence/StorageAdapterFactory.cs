using Microsoft.EntityFrameworkCore;
using Rang.SkillTracking.Application.Boundary.Output;
using Rang.SkillTracking.Domain.Employees;
using Rang.SkillTracking.Domain.Skills;
using Rang.SkillTracking.Persistence.Ef.Context;
using System.Linq;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Persistence
{
    public static class StorageAdapterFactory
    {
        public static async Task<IStorageAdapter> CreateNewStorageAdapterAsync(DbContextOptions<SkillTrackingDbContext> contextOptions, StorageAdapterInitializer storageAdapterInitializer = null)
        {
            var skillTrackingDbContext = new SkillTrackingDbContext(contextOptions);

            if (storageAdapterInitializer == null)
                return new StorageAdapter(skillTrackingDbContext);

            await InitializeDbContextAsync(skillTrackingDbContext, storageAdapterInitializer);
            return new StorageAdapter(skillTrackingDbContext);
        }

        private static async Task InitializeDbContextAsync(SkillTrackingDbContext skillTrackingDbContext, StorageAdapterInitializer storageInitializer)
        {
            if(storageInitializer.Employees != null)
                await skillTrackingDbContext.EmployeeModelSet.AddRangeAsync(storageInitializer.Employees.Select(emp => emp.GetModel()));

            if (storageInitializer.EvaluationPeriods != null)
                await skillTrackingDbContext.EvaluationPeriodInUctModelSet.AddRangeAsync(storageInitializer.EvaluationPeriods.Select(ep => ep.GetModel()));

            await skillTrackingDbContext.SaveChangesAsync();
        }
    }

    public class StorageAdapterInitializer
    {
        public Employee[] Employees { get; set; }
        public EvaluationPeriod[] EvaluationPeriods { get; set; }
    }
}
