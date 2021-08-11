using Rang.SkillTracking.Application.Boundary.Output;
using Rang.SkillTracking.Application.Common;
using Rang.SkillTracking.Application.SkillEvaluation;
using Rang.SkillTracking.Domain.Employees;
using Rang.SkillTracking.Domain.Skills;
using Rang.SkillTracking.Persistence;
using Rang.SkillTracking.Tests.xUnit.TestDoubles;
using Rang.SkillTracking.Tests.xUnit.TestDoubles.Fakes;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Rang.SkillTracking.Tests.xUnit.IntegrationTests
{
    public class AddNewEvaluationToEvaluateeTests
    {
        private readonly ITestOutputHelper _output;

        public AddNewEvaluationToEvaluateeTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenCreateInstanceOfAddNewEvaluationToEvaluateeIfStorageIsNull()
        {
            // arrange
            IStoragePort storageAdapter = null;
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);
            
            // act
            void action() => new AddNewEvaluationToEvaluateeUseCase(storageAdapter, presenterAdapter);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public async Task ShouldThrowArgumentNullExceptionWhenCreateInstanceOfAddNewEvaluationToEvaluateeIfEvaluationPeriodModelIsNull()
        {
            // arrange
            IStoragePort storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync();
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);
            var useCase = new AddNewEvaluationToEvaluateeUseCase(storageAdapter, presenterAdapter);
            uint employeeNumberofEvaluatee = 101;
            EvaluationPeriodModel evaluationPeriodModel = null;

            // act
            Task action() => useCase.AddNewEvaluationToEvaluateeAsync(employeeNumberofEvaluatee, evaluationPeriodModel);

            // assert
            Assert.ThrowsAsync<ArgumentNullException>(action);
        }

        [Fact]
        public async Task ShouldReturnEvaluateeNotFoundWhenAddNewEvaluationToEvaluateeIfEmployeeDoesNotExistInStorage()
        {
            // arrange
            IStoragePort storageAdapter = await StorageAdapterFakeFactory .CreateInMemoryStorageAdapterAsync();
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);
            uint employeeNumberofEvaluatee = 101;
            var evaluationPeriodModel = new EvaluationPeriodModel { TimeZoneInfo = TimeZoneInfo.Local, StartDate = new DateTime(DateTime.Today.Year, 1, 1) , EndDate = new DateTime(DateTime.Today.Year, 12, 31) };
            var useCase = new AddNewEvaluationToEvaluateeUseCase(storageAdapter, presenterAdapter);

            // act
            var result = await useCase.AddNewEvaluationToEvaluateeAsync(employeeNumberofEvaluatee, evaluationPeriodModel);

            // assert
            Assert.Equal(UseCaseResultStatusCode.EvaluateeNotFound, result.StatusCode);
        }

        [Fact]
        public async Task ShouldAddNewEvaluationToEvaluatee()
        {
            // arrange
            StorageAdapterInitializer storageAdapterInitializer = new()
            {
                Employees = new Employee[] { new Employee(101, "John Doe") },
                EvaluationPeriods = new EvaluationPeriod[] { new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31)) }
            };
            IStoragePort storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync(storageAdapterInitializer);
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);
            uint employeeNumberofEvaluatee = 101;
            var evaluationPeriodModel = new EvaluationPeriodModel { TimeZoneInfo = TimeZoneInfo.Local, StartDate = new DateTime(DateTime.Today.Year, 1, 1), EndDate = new DateTime(DateTime.Today.Year, 12, 31) };
            var useCase = new AddNewEvaluationToEvaluateeUseCase(storageAdapter, presenterAdapter);

            // act
            var result = await useCase.AddNewEvaluationToEvaluateeAsync(employeeNumberofEvaluatee, evaluationPeriodModel);

            // assert
            Assert.NotNull(((PresenterAdapterForTestOutput)presenterAdapter).SuccessMessage);
            Assert.Equal(UseCaseResultStatusCode.Success, result.StatusCode);
            Assert.Single(result.OutputModel.EvaluationModels);
            Assert.Equal(employeeNumberofEvaluatee, result.OutputModel.EvaluationModels.First().EvaluateeModel.EmployeeNumber);
            Assert.Equal(new DateTime(DateTime.Today.Year, 1, 1), result.OutputModel.EvaluationModels.First().EvaluationPeriodModel.StartDate.Date);
            Assert.Equal(new DateTime(DateTime.Today.Year, 12, 31), result.OutputModel.EvaluationModels.First().EvaluationPeriodModel.EndDate.Date);
        }
    }
}
