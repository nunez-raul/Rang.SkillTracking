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
        private readonly EvaluateeModel _evaluateeModel;
        private readonly EvaluationPeriodModel _evaluationPeriodModel;

        // properties
        public Evaluatee Evaluatee { get; private set; }
        public Evaluation Evaluation { get; private set; }


        // constructors
        public AddNewEvaluationToEvaluatee(IStorageAdapter storageAdapter, EvaluateeModel evaluateeModel, EvaluationPeriodModel evaluationPeriodModel)
            : base(storageAdapter)
        {
            _evaluateeModel = evaluateeModel ?? throw new ArgumentNullException(nameof(evaluateeModel));
            _evaluationPeriodModel = evaluationPeriodModel ?? throw new ArgumentNullException(nameof(evaluationPeriodModel));
        }

        // methods
        public async Task<UseCaseResult<EvaluateeModel>>  RunAsync()
        {
            if (!ValidateModels())
                return CreateInvalidInputModelsResult();

            if (!await LoadEvaluateeByEmployeeModel())
                return CreateNotFoundInputModelResult();

            var evaluationPeriod = await GetEvaluationPeriodByEvaluationPeriodModel();
            if (evaluationPeriod == null)
                throw new NotImplementedException(); // <-- what to return here?

            var result = Evaluatee.AddNewEvaluation(evaluationPeriod);
            if (result.OperationStatusCode != Domain.Common.OperationStatusCode.Success)
                throw new NotImplementedException(); // <-- what to return here?

            var evaluateeModel = await _storageAdapter.SaveEvaluateeAsync(Evaluatee);
            return CreateSucessResult(evaluateeModel.GetModel());
        }

        private bool ValidateModels()
        {
            
            return true;
        }

        private async Task<bool> LoadEvaluateeByEmployeeModel()
        {
            Evaluatee = await _storageAdapter.GetEvaluateeByEmployeeNumberAsync(_evaluateeModel.EmployeeModel.Number);

            if (Evaluatee == null)
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
                OutputModel = _evaluateeModel
            };
        }

        private UseCaseResult<EvaluateeModel> CreateNotFoundInputModelResult()
        {
            return new UseCaseResult<EvaluateeModel>
            {
                StatusCode = UseCaseResultStatusCode.NotFoundInputModel,
                OutputModel = _evaluateeModel
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
