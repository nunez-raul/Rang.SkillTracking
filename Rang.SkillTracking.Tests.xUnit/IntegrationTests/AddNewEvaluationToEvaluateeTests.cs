using Rang.SkillTracking.Application.Boundary.Output;
using Rang.SkillTracking.Application.Common;
using Rang.SkillTracking.Application.SkillEvaluation;
using Rang.SkillTracking.Domain.Employees;
using Rang.SkillTracking.Domain.Skills;
using Rang.SkillTracking.Persistence;
using Rang.SkillTracking.Tests.xUnit.TestDoubles;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Rang.SkillTracking.Tests.xUnit.IntegrationTests
{
    public class AddNewEvaluationToEvaluateeTests
    {
        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenCreateInstanceOfAddNewEvaluationToEvaluateeIfStorageIsNull()
        {
            // arrange
            IStorageAdapter storageAdapter = null;
            var targetEvaluateeModel = new EvaluateeModel(new EmployeeModel { Number = 101, Name = "John Doe" });
            var evaluationPeriodModel = new EvaluationPeriodModel { TimeZoneInfo = TimeZoneInfo.Local, StartDate = new DateTime(DateTime.Today.Year, 1, 1), EndDate = new DateTime(DateTime.Today.Year, 12, 31) };
            

            // act
            void action() => new AddNewEvaluationToEvaluatee(storageAdapter, targetEvaluateeModel, evaluationPeriodModel);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public async Task ShouldThrowArgumentNullExceptionWhenCreateInstanceOfAddNewEvaluationToEvaluateeIfEvaluateeModelIsNull()
        {
            // arrange
            IStorageAdapter storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync();
            EvaluateeModel targetEvaluateeModel = null;
            var evaluationPeriodModel = new EvaluationPeriodModel { TimeZoneInfo = TimeZoneInfo.Local, StartDate = new DateTime(DateTime.Today.Year, 1, 1), EndDate = new DateTime(DateTime.Today.Year, 12, 31) };


            // act
            void action() => new AddNewEvaluationToEvaluatee(storageAdapter, targetEvaluateeModel, evaluationPeriodModel);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public async Task ShouldThrowArgumentNullExceptionWhenCreateInstanceOfAddNewEvaluationToEvaluateeIfEvaluationPeriodModelIsNull()
        {
            // arrange
            IStorageAdapter storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync();
            var targetEvaluateeModel = new EvaluateeModel(new EmployeeModel { Number = 101, Name = "John Doe" });
            EvaluationPeriodModel evaluationPeriodModel = null;


            // act
            void action() => new AddNewEvaluationToEvaluatee(storageAdapter, targetEvaluateeModel, evaluationPeriodModel);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public async Task ShouldReturnNotFoundInputModelWhenAddNewEvaluationToEvaluateeIfEmployeeDoesNotExistInStorage()
        {
            // arrange
            IStorageAdapter storageAdapter = await StorageAdapterFakeFactory .CreateInMemoryStorageAdapterAsync();
            var targetEvaluateeModel = new EvaluateeModel(new EmployeeModel { Number = 101, Name = "John Doe" });
            var evaluationPeriodModel = new EvaluationPeriodModel { TimeZoneInfo = TimeZoneInfo.Local, StartDate = new DateTime(DateTime.Today.Year, 1, 1) , EndDate = new DateTime(DateTime.Today.Year, 12, 31) };
            var useCase = new AddNewEvaluationToEvaluatee(storageAdapter, targetEvaluateeModel, evaluationPeriodModel);

            // act
            var result = await useCase.RunAsync();

            // assert
            Assert.Equal(UseCaseResultStatusCode.NotFoundInputModel, result.StatusCode);
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
            IStorageAdapter storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync(storageAdapterInitializer);
            
            var targetEvaluateeModel = new EvaluateeModel(new EmployeeModel { Number = 101, Name = "John Doe" });
            var evaluationPeriodModel = new EvaluationPeriodModel { TimeZoneInfo = TimeZoneInfo.Local, StartDate = new DateTime(DateTime.Today.Year, 1, 1), EndDate = new DateTime(DateTime.Today.Year, 12, 31) };
            var useCase = new AddNewEvaluationToEvaluatee(storageAdapter, targetEvaluateeModel, evaluationPeriodModel);

            // act
            var result = await useCase.RunAsync();

            // assert
            Assert.Equal(UseCaseResultStatusCode.Success, result.StatusCode);
            Assert.Single(result.OutputModel.EvaluationModels);
            Assert.Equal("101", result.OutputModel.EvaluationModels.First().EvaluateeModel.EmployeeNumber.ToString());
            Assert.Equal(new DateTime(DateTime.Today.Year, 1, 1), result.OutputModel.EvaluationModels.First().EvaluationPeriodModel.StartDate.Date);
            Assert.Equal(new DateTime(DateTime.Today.Year, 12, 31), result.OutputModel.EvaluationModels.First().EvaluationPeriodModel.EndDate.Date);
        }
    }
}
