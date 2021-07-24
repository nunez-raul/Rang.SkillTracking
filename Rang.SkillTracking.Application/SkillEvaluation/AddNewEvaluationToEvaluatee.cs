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
        private UseCaseResult<EvaluateeModel> EvaluateeNotFoundResult { get => 
                new UseCaseResult<EvaluateeModel> {
                    StatusCode = UseCaseResultStatusCode.EvaluateeNotFound,
                    OutputModel = null};
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

            if (!await LoadEvaluateeBySuppliedEmployeeNumber())
                return EvaluateeNotFoundResult;

            var evaluationPeriod = await GetEvaluationPeriodByEvaluationPeriodModel();
            if (evaluationPeriod == null)
                throw new NotImplementedException(); // <-- what to return here?

            var result = LoadedEvaluatee.AddNewEvaluation(evaluationPeriod);
            if (result.OperationStatusCode != Domain.Common.OperationStatusCode.Success)
                throw new NotImplementedException(); // <-- what to return here?

            var evaluateeModel = await _storageAdapter.SaveEvaluateeAsync(LoadedEvaluatee);
            return CreateSucessResult(evaluateeModel.GetModel());
        }

        private async Task<bool> LoadEvaluateeBySuppliedEmployeeNumber()
        {
            LoadedEvaluatee = await _storageAdapter.GetEvaluateeByEmployeeNumberAsync(_employeeNumber);

            if (LoadedEvaluatee == null)
                return false;
           return true;
        }

        private async Task<EvaluationPeriod> GetEvaluationPeriodByEvaluationPeriodModel()
        {
            return await _storageAdapter.GetEvaluationPeriodByStartDateAsync(_evaluationPeriodModel.TimeZoneInfo, _evaluationPeriodModel.StartDate);
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
