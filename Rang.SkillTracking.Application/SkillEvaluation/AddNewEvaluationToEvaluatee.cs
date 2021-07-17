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
        private readonly EvaluateeModel _targetEvaluateeModel;
        private readonly EvaluationPeriodModel _evaluationPeriodModel;

        // properties
        public Evaluatee LoadedEvaluatee { get; private set; }
      
        // constructors
        public AddNewEvaluationToEvaluatee(IStorageAdapter storageAdapter, EvaluateeModel targetEvaluateeModel, EvaluationPeriodModel evaluationPeriodModel)
            : base(storageAdapter)
        {
            _targetEvaluateeModel = targetEvaluateeModel ?? throw new ArgumentNullException(nameof(targetEvaluateeModel));
            _evaluationPeriodModel = evaluationPeriodModel ?? throw new ArgumentNullException(nameof(evaluationPeriodModel));
        }

        // methods
        public async Task<UseCaseResult<EvaluateeModel>>  RunAsync()
        {
            var targetEvaluatee = new Evaluatee(_targetEvaluateeModel);

            if (! targetEvaluatee.IsValid)
                return CreateInvalidInputModelsResult();

            if (!await LoadEvaluateeByEmployeeModel())
                return CreateNotFoundInputModelResult();

            var evaluationPeriod = await GetEvaluationPeriodByEvaluationPeriodModel();
            if (evaluationPeriod == null)
                throw new NotImplementedException(); // <-- what to return here?

            var result = LoadedEvaluatee.AddNewEvaluation(evaluationPeriod);
            if (result.OperationStatusCode != Domain.Common.OperationStatusCode.Success)
                throw new NotImplementedException(); // <-- what to return here?

            var evaluateeModel = await _storageAdapter.SaveEvaluateeAsync(LoadedEvaluatee);
            return CreateSucessResult(evaluateeModel.GetModel());
        }

        private async Task<bool> LoadEvaluateeByEmployeeModel()
        {
            LoadedEvaluatee = await _storageAdapter.GetEvaluateeByEmployeeNumberAsync(_targetEvaluateeModel.EmployeeModel.Number);

            if (LoadedEvaluatee == null)
                return false;
           return true;
        }

        private async Task<EvaluationPeriod> GetEvaluationPeriodByEvaluationPeriodModel()
        {
            return await _storageAdapter.GetEvaluationPeriodByStartDateAsync(_evaluationPeriodModel.TimeZoneInfo, _evaluationPeriodModel.StartDate);
        }

        private UseCaseResult<EvaluateeModel> CreateInvalidInputModelsResult()
        {
            return new UseCaseResult<EvaluateeModel>
            {
                StatusCode = UseCaseResultStatusCode.InvalidInputModel,
                OutputModel = _targetEvaluateeModel
            };
        }

        private UseCaseResult<EvaluateeModel> CreateNotFoundInputModelResult()
        {
            return new UseCaseResult<EvaluateeModel>
            {
                StatusCode = UseCaseResultStatusCode.NotFoundInputModel,
                OutputModel = _targetEvaluateeModel
            };
        }

        private UseCaseResult<EvaluateeModel> CreateSucessResult(EvaluateeModel outputModel)
        {
            return new UseCaseResult<EvaluateeModel>
            {
                StatusCode = UseCaseResultStatusCode.Success,
                OutputModel = outputModel
            };
        }
    }
}
