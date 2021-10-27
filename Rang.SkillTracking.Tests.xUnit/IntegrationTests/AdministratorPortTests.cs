using Rang.SkillTracking.Application;
using Rang.SkillTracking.Application.Boundary.Input;
using Rang.SkillTracking.Application.Boundary.Output;
using Rang.SkillTracking.Application.Common;
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
    public class AdministratorPortTests
    {
        private readonly ITestOutputHelper _output;

        public AdministratorPortTests(ITestOutputHelper output)
        {
            _output = output;
        }

        // AdministratorInteractor constructor
        [Fact]
        public async Task ShouldThrowArgumentNullExceptionWhenCreateInstanceOfAdministratorInteractorIfPresenterIsNull()
        {
            // arrange
            IStoragePort storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync(); ;
            IPresenterPort presenterAdapter = null;

            // act
            void action() => new AdministratorInteractor(storageAdapter, presenterAdapter);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionWhenCreateInstanceOfAdministratorInteractorIfStorageAdapterIsNull()
        {
            // arrange
            IStoragePort storageAdapter = null;
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);

            // act
            void action() => new AdministratorInteractor(storageAdapter, presenterAdapter);

            // assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public async Task ShouldReturnInstanceWhenCreateInstanceOfAdministratorInteractor()
        {
            // arrange
            IStoragePort storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync();
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);

            // act
            IAdministratorPort interactor = new AdministratorInteractor(storageAdapter, presenterAdapter);

            // assert
            Assert.NotNull(interactor);
        }

        // AddNewEvaluationPeriod
        [Fact]
        public async Task ShouldAddNewEvaluationPeriodIfNoExistingEvaluationPeriods()
        {
            /*****************************************************
            Testing for:
            NP: ---| ---|                > Only one, not overlappable!
            ******************************************************/

            // arrange
            IStoragePort storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync();
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);
            IAdministratorPort interactor = new AdministratorInteractor(storageAdapter, presenterAdapter);

            var evaluationPeriodModel = new EvaluationPeriodModel { TimeZoneInfo = TimeZoneInfo.Local, StartDate = new DateTime(DateTime.Today.Year, 1, 1, 0, 0, 0, 0), EndDate = new DateTime(DateTime.Today.Year, 12, 31, 23, 59, 59, 999) };

            // act
            var result = await interactor.AddNewEvaluationPeriodAsync(evaluationPeriodModel);

            // assert
            Assert.Equal(UseCaseResultStatusCode.Success, result.StatusCode);
        }

        [Fact]
        public async Task ShouldAddNewEvaluationPeriodIfNotOverlapWithExistingPeriod1()
        {
            /*****************************************************
            Testing for:
            NP: -------|---|
            EP: ---|---|                > No Overlapping
            ******************************************************/

            // arrange
            StorageAdapterInitializer storageAdapterInitializer = new()
            {
                //previous year
                EvaluationPeriods = new EvaluationPeriod[] { new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year - 1, 1, 1, 0, 0, 0, 0), new DateTime(DateTime.Today.Year - 1, 12, 31, 23, 59, 59, 999)) }
            };
            IStoragePort storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync(storageAdapterInitializer);
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);
            IAdministratorPort interactor = new AdministratorInteractor(storageAdapter, presenterAdapter);
            //current year
            var evaluationPeriodModel = new EvaluationPeriodModel { TimeZoneInfo = TimeZoneInfo.Local, StartDate = new DateTime(DateTime.Today.Year, 1, 1, 0, 0, 0, 0), EndDate = new DateTime(DateTime.Today.Year, 12, 31, 23, 59, 59, 999) };

            // act
            var result = await interactor.AddNewEvaluationPeriodAsync(evaluationPeriodModel);

            // assert
            Assert.Equal(UseCaseResultStatusCode.Success, result.StatusCode);
        }

        [Fact]
        public async Task ShouldAddNewEvaluationPeriodIfNotOverlapWithExistingPeriod2()
        {
            /*****************************************************
             Testing for:
             NP: --------|---|
             EP: ---|---|                > No Overlapping
             ******************************************************/

            // arrange
            StorageAdapterInitializer storageAdapterInitializer = new()
            {
                //previous year
                EvaluationPeriods = new EvaluationPeriod[] { new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year - 1, 1, 1, 0, 0, 0, 0), new DateTime(DateTime.Today.Year - 1, 12, 31, 23, 59, 59, 999)) }
            };
            IStoragePort storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync(storageAdapterInitializer);
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);
            IAdministratorPort interactor = new AdministratorInteractor(storageAdapter, presenterAdapter);
            //current year since february
            var evaluationPeriodModel = new EvaluationPeriodModel { TimeZoneInfo = TimeZoneInfo.Local, StartDate = new DateTime(DateTime.Today.Year, 2, 1, 0, 0, 0, 0), EndDate = new DateTime(DateTime.Today.Year, 12, 31, 23, 59, 59, 999) };

            // act
            var result = await interactor.AddNewEvaluationPeriodAsync(evaluationPeriodModel);

            // assert
            Assert.Equal(UseCaseResultStatusCode.Success, result.StatusCode);
        }

        [Fact]
        public async Task ShouldAddNewEvaluationPeriodIfNotOverlapWithExistingPeriod3()
        {
            /*****************************************************
            Testing for:
            NP: --|---|
            EP: ------|---|             > No Overlapping
            ******************************************************/

            // arrange
            StorageAdapterInitializer storageAdapterInitializer = new()
            {
                //current year
                EvaluationPeriods = new EvaluationPeriod[] { new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1, 0, 0, 0, 0), new DateTime(DateTime.Today.Year, 12, 31, 23, 59, 59, 999)) }
            };
            IStoragePort storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync(storageAdapterInitializer);
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);
            IAdministratorPort interactor = new AdministratorInteractor(storageAdapter, presenterAdapter);
            //previous year
            var evaluationPeriodModel = new EvaluationPeriodModel { TimeZoneInfo = TimeZoneInfo.Local, StartDate = new DateTime(DateTime.Today.Year -1, 1, 1, 0, 0, 0, 0), EndDate = new DateTime(DateTime.Today.Year-1, 12, 31, 23, 59, 59, 999) };

            // act
            var result = await interactor.AddNewEvaluationPeriodAsync(evaluationPeriodModel);

            // assert
            Assert.Equal(UseCaseResultStatusCode.Success, result.StatusCode);
        }

        [Fact]
        public async Task ShouldAddNewEvaluationPeriodIfNotOverlapWithExistingPeriod4()
        {
            /*****************************************************
            Testing for:
            NP: ---|---|
            EP: --------|---|            > No Overlapping
            ******************************************************/

            // arrange
            StorageAdapterInitializer storageAdapterInitializer = new()
            {
                //current year
                EvaluationPeriods = new EvaluationPeriod[] { new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1, 0, 0, 0, 0), new DateTime(DateTime.Today.Year, 12, 31, 23, 59, 59, 999)) }
            };
            IStoragePort storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync(storageAdapterInitializer);
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);
            IAdministratorPort interactor = new AdministratorInteractor(storageAdapter, presenterAdapter);
            //previous year ending one day early
            var evaluationPeriodModel = new EvaluationPeriodModel { TimeZoneInfo = TimeZoneInfo.Local, StartDate = new DateTime(DateTime.Today.Year - 1, 1, 1, 0, 0, 0, 0), EndDate = new DateTime(DateTime.Today.Year - 1, 12, 30, 23, 59, 59, 999) };

            // act
            var result = await interactor.AddNewEvaluationPeriodAsync(evaluationPeriodModel);

            // assert
            Assert.Equal(UseCaseResultStatusCode.Success, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnPeriodOverlapWhenAddNewEvaluationPeriodIfOverlapWithExistingPeriod1()
        {
            /*****************************************************
            Testing for:
            NP: ------|---|
            EP: ---|---|                > Overlap!!
            ******************************************************/

            // arrange
            StorageAdapterInitializer storageAdapterInitializer = new()
            {
                //current year
                EvaluationPeriods = new EvaluationPeriod[] { new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1, 0, 0, 0, 0), new DateTime(DateTime.Today.Year, 12, 31, 23, 59, 59, 999)) }
            };
            IStoragePort storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync(storageAdapterInitializer);
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);
            IAdministratorPort interactor = new AdministratorInteractor(storageAdapter, presenterAdapter);
            //next year starting one day early
            var evaluationPeriodModel = new EvaluationPeriodModel { TimeZoneInfo = TimeZoneInfo.Local, StartDate = new DateTime(DateTime.Today.Year, 12, 31, 0, 0, 0, 0), EndDate = new DateTime(DateTime.Today.Year + 1, 12, 31, 23, 59, 59, 999) };

            // act
            var result = await interactor.AddNewEvaluationPeriodAsync(evaluationPeriodModel);

            // assert
            Assert.Equal(UseCaseResultStatusCode.EvaluationPeriodOverlap, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnPeriodOverlapWhenAddNewEvaluationPeriodIfOverlapWithExistingPeriod2()
        {
            /*****************************************************
            Testing for:
            NP: ---|----|
            EP: ---|---|                > Overlap!!
            ******************************************************/

            // arrange
            StorageAdapterInitializer storageAdapterInitializer = new()
            {
                //current year
                EvaluationPeriods = new EvaluationPeriod[] { new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1, 0, 0, 0, 0), new DateTime(DateTime.Today.Year, 12, 31, 23, 59, 59, 999)) }
            };
            IStoragePort storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync(storageAdapterInitializer);
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);
            IAdministratorPort interactor = new AdministratorInteractor(storageAdapter, presenterAdapter);
            //current year + one day
            var evaluationPeriodModel = new EvaluationPeriodModel { TimeZoneInfo = TimeZoneInfo.Local, StartDate = new DateTime(DateTime.Today.Year, 1, 1, 0, 0, 0, 0), EndDate = new DateTime(DateTime.Today.Year + 1, 1, 1, 23, 59, 59, 999) };

            // act
            var result = await interactor.AddNewEvaluationPeriodAsync(evaluationPeriodModel);

            // assert
            Assert.Equal(UseCaseResultStatusCode.EvaluationPeriodOverlap, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnPeriodOverlapWhenAddNewEvaluationPeriodIfOverlapWithExistingPeriod3()
        {
            /*****************************************************
            Testing for:
            NP: ----|---|
            EP: ---|-----|                > Overlap!!
            ******************************************************/

            // arrange
            StorageAdapterInitializer storageAdapterInitializer = new()
            {
                //current year
                EvaluationPeriods = new EvaluationPeriod[] { new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1, 0, 0, 0, 0), new DateTime(DateTime.Today.Year, 12, 31, 23, 59, 59, 999)) }
            };
            IStoragePort storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync(storageAdapterInitializer);
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);
            IAdministratorPort interactor = new AdministratorInteractor(storageAdapter, presenterAdapter);
            //within current year
            var evaluationPeriodModel = new EvaluationPeriodModel { TimeZoneInfo = TimeZoneInfo.Local, StartDate = new DateTime(DateTime.Today.Year, 1, 5, 0, 0, 0, 0), EndDate = new DateTime(DateTime.Today.Year, 10, 31, 23, 59, 59, 999) };

            // act
            var result = await interactor.AddNewEvaluationPeriodAsync(evaluationPeriodModel);

            // assert
            Assert.Equal(UseCaseResultStatusCode.EvaluationPeriodOverlap, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnPeriodOverlapWhenAddNewEvaluationPeriodIfOverlapWithExistingPeriod4()
        {
            /*****************************************************
             Testing for:
             NP: --|-----|
             EP: ---|---|                > Overlap!!
             ******************************************************/

            // arrange
            StorageAdapterInitializer storageAdapterInitializer = new()
            {
                //current year
                EvaluationPeriods = new EvaluationPeriod[] { new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1, 0, 0, 0, 0), new DateTime(DateTime.Today.Year, 12, 31, 23, 59, 59, 999)) }
            };
            IStoragePort storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync(storageAdapterInitializer);
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);
            IAdministratorPort interactor = new AdministratorInteractor(storageAdapter, presenterAdapter);
            //within current year
            var evaluationPeriodModel = new EvaluationPeriodModel { TimeZoneInfo = TimeZoneInfo.Local, StartDate = new DateTime(DateTime.Today.Year -1, 1, 1, 0, 0, 0, 0), EndDate = new DateTime(DateTime.Today.Year+1, 12, 31, 23, 59, 59, 999) };

            // act
            var result = await interactor.AddNewEvaluationPeriodAsync(evaluationPeriodModel);

            // assert
            Assert.Equal(UseCaseResultStatusCode.EvaluationPeriodOverlap, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnPeriodOverlapWhenAddNewEvaluationPeriodIfOverlapWithExistingPeriod5()
        {
            /*****************************************************
            Testing for:
            NP: --|----|
            EP: ---|---|                > Overlap!!
            ******************************************************/

            // arrange
            StorageAdapterInitializer storageAdapterInitializer = new()
            {
                //current year
                EvaluationPeriods = new EvaluationPeriod[] { new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1, 0, 0, 0, 0), new DateTime(DateTime.Today.Year, 12, 31, 23, 59, 59, 999)) }
            };
            IStoragePort storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync(storageAdapterInitializer);
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);
            IAdministratorPort interactor = new AdministratorInteractor(storageAdapter, presenterAdapter);
            //within current year
            var evaluationPeriodModel = new EvaluationPeriodModel { TimeZoneInfo = TimeZoneInfo.Local, StartDate = new DateTime(DateTime.Today.Year - 1, 1, 1, 0, 0, 0, 0), EndDate = new DateTime(DateTime.Today.Year, 12, 31, 23, 59, 59, 999) };

            // act
            var result = await interactor.AddNewEvaluationPeriodAsync(evaluationPeriodModel);

            // assert
            Assert.Equal(UseCaseResultStatusCode.EvaluationPeriodOverlap, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnPeriodOverlapWhenAddNewEvaluationPeriodIfOverlapWithExistingPeriod6()
        {
            /*****************************************************
            Testing for:
            NP: ---|---|
            EP: ---|---|                > Overlap!!
            ******************************************************/

            // arrange
            StorageAdapterInitializer storageAdapterInitializer = new()
            {
                //current year
                EvaluationPeriods = new EvaluationPeriod[] { new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1, 0, 0, 0, 0), new DateTime(DateTime.Today.Year, 12, 31, 23, 59, 59, 999)) }
            };
            IStoragePort storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync(storageAdapterInitializer);
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);
            IAdministratorPort interactor = new AdministratorInteractor(storageAdapter, presenterAdapter);
            //within current year
            var evaluationPeriodModel = new EvaluationPeriodModel { TimeZoneInfo = TimeZoneInfo.Local, StartDate = new DateTime(DateTime.Today.Year, 1, 1, 0, 0, 0, 0), EndDate = new DateTime(DateTime.Today.Year, 12, 31, 23, 59, 59, 999) };

            // act
            var result = await interactor.AddNewEvaluationPeriodAsync(evaluationPeriodModel);

            // assert
            Assert.Equal(UseCaseResultStatusCode.EvaluationPeriodOverlap, result.StatusCode);
        }

        [Fact]
        public async Task ShouldReturnPeriodOverlapWhenAddNewEvaluationPeriodIfOverlapWithExistingPeriod7()
        {
            /*****************************************************
            Testing for:
            NP: --|---|
            EP: ---|---|                > Overlap!!
            ******************************************************/

            // arrange
            StorageAdapterInitializer storageAdapterInitializer = new()
            {
                //current year
                EvaluationPeriods = new EvaluationPeriod[] { new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1, 0, 0, 0, 0), new DateTime(DateTime.Today.Year, 12, 31, 23, 59, 59, 999)) }
            };
            IStoragePort storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync(storageAdapterInitializer);
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);
            IAdministratorPort interactor = new AdministratorInteractor(storageAdapter, presenterAdapter);
            //within current year
            var evaluationPeriodModel = new EvaluationPeriodModel { TimeZoneInfo = TimeZoneInfo.Local, StartDate = new DateTime(DateTime.Today.Year-1, 12, 31, 0, 0, 0, 0), EndDate = new DateTime(DateTime.Today.Year, 12, 30, 23, 59, 59, 999) };

            // act
            var result = await interactor.AddNewEvaluationPeriodAsync(evaluationPeriodModel);

            // assert
            Assert.Equal(UseCaseResultStatusCode.EvaluationPeriodOverlap, result.StatusCode);
        }

        [Fact]
        public async Task ShouldThrowArgumentNullExceptionWhenAddNewEvaluationPeriodIfEvaluationPeriodModelIsNull()
        {
            // arrange
            StorageAdapterInitializer storageAdapterInitializer = new()
            {
                //current year
                EvaluationPeriods = new EvaluationPeriod[] { new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1, 0, 0, 0, 0), new DateTime(DateTime.Today.Year, 12, 31, 23, 59, 59, 999)) }
            };
            IStoragePort storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync(storageAdapterInitializer);
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);
            IAdministratorPort interactor = new AdministratorInteractor(storageAdapter, presenterAdapter);
            EvaluationPeriodModel evaluationPeriodModel = null;

            // act
            Task<UseCaseResult<EvaluationPeriodModel>> action() => interactor.AddNewEvaluationPeriodAsync(evaluationPeriodModel);

            // assert
            await Assert.ThrowsAsync<ArgumentNullException>(action);
        }

        // AddNewEvaluationToEvaluatee
        [Fact]
        public async Task ShouldAddNewEvaluationToEvaluateeAsync()
        {
            // arrange
            StorageAdapterInitializer storageAdapterInitializer = new()
            {
                Employees = new Employee[] { new Employee(101, "John Doe") },
                EvaluationPeriods = new EvaluationPeriod[] { new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31)) }
            };
            IStoragePort storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync(storageAdapterInitializer);
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);
            IAdministratorPort interactor = new AdministratorInteractor(storageAdapter, presenterAdapter);

            uint employeeNumberofEvaluatee = 101;
            var evaluationPeriodModel = new EvaluationPeriodModel { TimeZoneInfo = TimeZoneInfo.Local, StartDate = new DateTime(DateTime.Today.Year, 1, 1), EndDate = new DateTime(DateTime.Today.Year, 12, 31) };


            // act
            var result = await interactor.AddNewEvaluationToEvaluateeAsync(employeeNumberofEvaluatee, evaluationPeriodModel);

            // assert
            Assert.NotNull(((PresenterAdapterForTestOutput)presenterAdapter).SuccessMessage);
            Assert.Equal(UseCaseResultStatusCode.Success, result.StatusCode);
            Assert.Single(result.OutputModel.EvaluationModels);
            Assert.Equal(employeeNumberofEvaluatee, result.OutputModel.EvaluationModels.First().EvaluateeModel.EmployeeNumber);
            Assert.Equal(new DateTime(DateTime.Today.Year, 1, 1), result.OutputModel.EvaluationModels.First().EvaluationPeriodModel.StartDate.Date);
            Assert.Equal(new DateTime(DateTime.Today.Year, 12, 31), result.OutputModel.EvaluationModels.First().EvaluationPeriodModel.EndDate.Date);
        }

        [Fact]
        public async Task ShouldReturnEvaluateeNotFoundWhenAddNewEvaluationToEvaluateeAsyncIfEmployeeDoesNotExistInStorage()
        {
            // arrange
            StorageAdapterInitializer storageAdapterInitializer = new()
            {
                Employees = new Employee[] { new Employee(101, "John Doe") },
                EvaluationPeriods = new EvaluationPeriod[] { new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31)) }
            };
            IStoragePort storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync(storageAdapterInitializer);
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);
            IAdministratorPort interactor = new AdministratorInteractor(storageAdapter, presenterAdapter);

            uint employeeNumberofEvaluatee = 102;
            var evaluationPeriodModel = new EvaluationPeriodModel { TimeZoneInfo = TimeZoneInfo.Local, StartDate = new DateTime(DateTime.Today.Year, 1, 1), EndDate = new DateTime(DateTime.Today.Year, 12, 31) };


            // act
            var result = await interactor.AddNewEvaluationToEvaluateeAsync(employeeNumberofEvaluatee, evaluationPeriodModel);

            // assert
            Assert.Equal(UseCaseResultStatusCode.EvaluateeNotFound, result.StatusCode);
        }

        [Fact]
        public async Task ShouldThrowArgumentNullExceptionWhenAddNewEvaluationToEvaluateeAsyncIfEvaluationPeriodModelIsNull()
        {
            // arrange
            StorageAdapterInitializer storageAdapterInitializer = new()
            {
                Employees = new Employee[] { new Employee(101, "John Doe") },
                EvaluationPeriods = new EvaluationPeriod[] { new EvaluationPeriod(TimeZoneInfo.Local, new DateTime(DateTime.Today.Year, 1, 1), new DateTime(DateTime.Today.Year, 12, 31)) }
            };
            IStoragePort storageAdapter = await StorageAdapterFakeFactory.CreateInMemoryStorageAdapterAsync(storageAdapterInitializer);
            IPresenterPort presenterAdapter = PresenterAdapterFakeFactory.CreatePresenterAdapterForTestOutput(_output);
            IAdministratorPort interactor = new AdministratorInteractor(storageAdapter, presenterAdapter);

            uint employeeNumberofEvaluatee = 101;
            EvaluationPeriodModel evaluationPeriodModel = null;


            // act
            Task<UseCaseResult<EvaluateeModel>> action() => interactor.AddNewEvaluationToEvaluateeAsync(employeeNumberofEvaluatee, evaluationPeriodModel);

            // assert
            await Assert.ThrowsAsync<ArgumentNullException>(action);
        }

    }
}
