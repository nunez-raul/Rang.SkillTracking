using Rang.SkillTracking.Application.Boundary.Output;
using Xunit.Abstractions;

namespace Rang.SkillTracking.Tests.xUnit.TestDoubles.Fakes
{
    internal class PresenterAdapterForTestOutput : IPresenterAdapter
    {
        // fields
        ITestOutputHelper _testOutput;
        string _errorMessage;
        string _message;
        string _successMessage;

        // properties
        internal string ErrorMessage { get => _errorMessage; }
        internal string Message { get => _message; }
        internal string SuccessMessage { get => _successMessage; }

        // constructors
        public PresenterAdapterForTestOutput(ITestOutputHelper testOutput)
        {
            _testOutput = testOutput;
        }

        // methods
        public void PresentErrorMessage(string message)
        {
            _errorMessage = message;
            _testOutput.WriteLine(message);
        }

        public void PresentMessage(string message)
        {
            _message = message;
            _testOutput.WriteLine(message);
        }

        public void PresentSuccessOperationMessage(string message)
        {
            _successMessage = message;
            _testOutput.WriteLine(message);
        }
    }
}
