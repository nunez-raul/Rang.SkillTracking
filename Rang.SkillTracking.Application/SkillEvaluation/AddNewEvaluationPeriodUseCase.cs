using Rang.SkillTracking.Application.Boundary.Output;
using Rang.SkillTracking.Application.Common;
using Rang.SkillTracking.Domain.Skills;
using System;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Application.SkillEvaluation
{
    internal class AddNewEvaluationPeriodUseCase : StorageDependentUseCase
    {
        // fileds
        private readonly IPresenterPort _presenterAdapter;

        // properties

        // constructors
        public AddNewEvaluationPeriodUseCase(IStoragePort storageAdapter, IPresenterPort presenterAdapter)
            : base(storageAdapter)
        {
            _presenterAdapter = presenterAdapter ?? throw new ArgumentNullException(nameof(presenterAdapter));
        }

        // methods
        public async Task<UseCaseResult<EvaluationPeriodModel>> RunAsync(EvaluationPeriodModel evaluationPeriodModel) 
        {
            if (evaluationPeriodModel == null)
                throw new ArgumentNullException(nameof(evaluationPeriodModel));

            var evaluationPeriodToAdd = new EvaluationPeriod(evaluationPeriodModel.TimeZoneInfo, evaluationPeriodModel.StartDate, evaluationPeriodModel.EndDate);
            if (!evaluationPeriodToAdd.IsValid)
                throw new NotFiniteNumberException(); //<-- what to return here?

            //check overlap in storage

            //add to storage
            var result = await _storageAdapter.AddNewEvaluationPeriodAsync(evaluationPeriodToAdd);
            _presenterAdapter.PresentSuccessOperationMessage($"The evaluation period ({evaluationPeriodToAdd.StartDate.ToShortDateString()} - {evaluationPeriodToAdd.EndDate.ToShortDateString()}) was added successfully.");
            return CreateSucessResult(result.GetModel());
        }

        private UseCaseResult<EvaluationPeriodModel> CreateSucessResult(EvaluationPeriodModel outputModel)
        {
            return new UseCaseResult<EvaluationPeriodModel>
            {
                StatusCode = UseCaseResultStatusCode.Success,
                OutputModel = outputModel
            };
        }

    }
}
