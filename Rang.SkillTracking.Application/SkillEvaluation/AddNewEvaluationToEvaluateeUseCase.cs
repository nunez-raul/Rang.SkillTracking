using Rang.SkillTracking.Application.Boundary.Input;
using Rang.SkillTracking.Application.Boundary.Output;
using Rang.SkillTracking.Application.Common;
using Rang.SkillTracking.Domain.Skills;
using System;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Application.SkillEvaluation
{
    public class AddNewEvaluationToEvaluateeUseCase: StorageDependentUseCase, IAdministratorPort
    {
        // fileds
        private readonly IPresenterAdapter _presenterAdapter;
        
        // properties
        public Evaluatee LoadedEvaluatee { get; private set; }
        public EvaluationPeriod LoadedEvaluationPeriod { get; private set; }
        private UseCaseResult<EvaluateeModel> EvaluateeNotFoundResult { get => new()
            { StatusCode = UseCaseResultStatusCode.EvaluateeNotFound, OutputModel = null };
        }
        private UseCaseResult<EvaluateeModel> EvaluationPeriodNotFoundResult { get => new()
            { StatusCode = UseCaseResultStatusCode.EvaluationPeriodNotFound, OutputModel = null };
        }

        // constructors
        public AddNewEvaluationToEvaluateeUseCase(IStorageAdapter storageAdapter, IPresenterAdapter presenterAdapter)
            : base(storageAdapter)
        {
            _presenterAdapter = presenterAdapter ?? throw new ArgumentNullException(nameof(presenterAdapter));
        }

        // methods
        public async Task<UseCaseResult<EvaluateeModel>>  AddNewEvaluationToEvaluateeAsync(uint employeeNumberOfEvaluatee, EvaluationPeriodModel evaluationPeriodModel)
        {
            if(evaluationPeriodModel == null)
                throw new ArgumentNullException(nameof(evaluationPeriodModel));

            if (employeeNumberOfEvaluatee <  1)
                throw new NotImplementedException(); // <-- what to return here?

            if (!await LoadEvaluateeFromStorageByItsEmployeeNumber(employeeNumberOfEvaluatee))
                return EvaluateeNotFoundResult;

            if (!await LoadEvaluationPeriodFromStorageByEvaluationPeriodModel(evaluationPeriodModel))
                return EvaluationPeriodNotFoundResult;

            var result = LoadedEvaluatee.AddNewEvaluation(LoadedEvaluationPeriod);
            if (result.OperationStatusCode != Domain.Common.OperationStatusCode.Success)
                throw new NotImplementedException(); // <-- what to return here?

            var evaluateeModel = await _storageAdapter.SaveEvaluateeAsync(LoadedEvaluatee);
            return CreateSucessResult(evaluateeModel.GetModel());
        }

        private async Task<bool> LoadEvaluateeFromStorageByItsEmployeeNumber(uint employeeNumberOfEvaluatee)
        {
            LoadedEvaluatee = await _storageAdapter.GetEvaluateeByEmployeeNumberAsync(employeeNumberOfEvaluatee);

            if (LoadedEvaluatee == null)
                return false;
           return true;
        }

        private async Task<bool> LoadEvaluationPeriodFromStorageByEvaluationPeriodModel(EvaluationPeriodModel evaluationPeriodModel)
        {
            LoadedEvaluationPeriod = await _storageAdapter.GetEvaluationPeriodByStartDateAsync(evaluationPeriodModel.TimeZoneInfo, evaluationPeriodModel.StartDate);

            if (LoadedEvaluationPeriod == null)
                return false;
            return true;
        }

        private UseCaseResult<EvaluateeModel> CreateSucessResult(EvaluateeModel outputModel)
        {
            _presenterAdapter.PresentSuccessOperationMessage("evaluation added successfully");
            return new UseCaseResult<EvaluateeModel>
            {
                StatusCode = UseCaseResultStatusCode.Success,
                OutputModel = outputModel
            };
        }
    }
}
