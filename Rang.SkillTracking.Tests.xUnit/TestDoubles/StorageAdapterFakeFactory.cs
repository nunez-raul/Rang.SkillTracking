using Microsoft.EntityFrameworkCore;
using Rang.SkillTracking.Application.Boundary.Output;
using Rang.SkillTracking.Persistence;
using Rang.SkillTracking.Persistence.Ef.Context;
using System;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Tests.xUnit.TestDoubles
{
    internal static class StorageAdapterFakeFactory
    {
        internal static async Task<IStorageAdapter> CreateInMemoryStorageAdapterAsync(StorageAdapterInitializer storageAdapterInitializer = null)
        {
            return await StorageAdapterFactory.CreateNewStorageAdapterAsync(GetInMemoryOptions(), storageAdapterInitializer);
        }

        private static DbContextOptions<SkillTrackingDbContext> GetInMemoryOptions()
        {
            return new DbContextOptionsBuilder<SkillTrackingDbContext>()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString())
                            .Options;
        }
    }
}
