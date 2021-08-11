using Rang.SkillTracking.Application.Boundary.Output;
using Rang.SkillTracking.Tests.xUnit.TestDoubles.Fakes;
using Xunit.Abstractions;

namespace Rang.SkillTracking.Tests.xUnit.TestDoubles
{
    internal static class PresenterAdapterFakeFactory
    {
        internal static IPresenterPort CreatePresenterAdapterForTestOutput(ITestOutputHelper testOutput)
        {
            return new PresenterAdapterForTestOutput(testOutput);
        }
    }
}
