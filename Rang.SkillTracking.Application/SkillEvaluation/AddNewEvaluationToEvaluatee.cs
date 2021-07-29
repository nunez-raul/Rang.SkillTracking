using Rang.SkillTracking.Application.Boundary.Output;
using Rang.SkillTracking.Application.Common;
using Rang.SkillTracking.Domain.Skills;
using System;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Application.SkillEvaluation
{
    public class AddNewEvaluationToEvaluatee: StorageDependentUseCase
    {
        // fileds
        private readonly IPresenterAdapter _presenterAdapter;
        private readonly uint _employeeNumber;
        private readonly EvaluationPeriodModel _evaluationPeriodModel;

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
        public AddNewEvaluationToEvaluatee(IStorageAdapter storageAdapter, IPresenterAdapter presenterAdapter, uint employeeNumberOfEvaluatee, EvaluationPeriodModel evaluationPeriodModel)
            : base(storageAdapter)
        {
            _presenterAdapter = presenterAdapter ?? throw new ArgumentNullException(nameof(presenterAdapter));
            _employeeNumber = employeeNumberOfEvaluatee;
            _evaluationPeriodModel = evaluationPeriodModel ?? throw new ArgumentNullException(nameof(evaluationPeriodModel));
        }

        // methods
        public async Task<UseCaseResult<EvaluateeModel>>  RunAsync()
        {
            if (_employeeNumber <  1)
                throw new NotImplementedException(); // <-- what to return here?

            if (!await LoadEvaluateeFromStorageByItsEmployeeNumber())
                return EvaluateeNotFoundResult;

            if (!await LoadEvaluationPeriodFromStorageByEvaluationPeriodModel())
                return EvaluationPeriodNotFoundResult;

            var result = LoadedEvaluatee.AddNewEvaluation(LoadedEvaluationPeriod);
            if (result.OperationStatusCode != Domain.Common.OperationStatusCode.Success)
                throw new NotImplementedException(); // <-- what to return here?

            var evaluateeModel = await _storageAdapter.SaveEvaluateeAsync(LoadedEvaluatee);
            return CreateSucessResult(evaluateeModel.GetModel());
        }

        private async Task<bool> LoadEvaluateeFromStorageByItsEmployeeNumber()
        {
            LoadedEvaluatee = await _storageAdapter.GetEvaluateeByEmployeeNumberAsync(_employeeNumber);

            if (LoadedEvaluatee == null)
                return false;
           return true;
        }

        private async Task<bool> LoadEvaluationPeriodFromStorageByEvaluationPeriodModel()
        {
            LoadedEvaluationPeriod = await _storageAdapter.GetEvaluationPeriodByStartDateAsync(_evaluationPeriodModel.TimeZoneInfo, _evaluationPeriodModel.StartDate);

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
