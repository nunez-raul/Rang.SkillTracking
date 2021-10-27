using Rang.SkillTracking.Application.Boundary.Input;
using Rang.SkillTracking.Application.Boundary.Output;
using Rang.SkillTracking.Application.Common;
using Rang.SkillTracking.Application.SkillEvaluation;
using Rang.SkillTracking.Domain.Skills;
using System;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Application
{
    public class AdministratorInteractor: IAdministratorPort
    {
        // fields
        private readonly IPresenterPort _presenterAdapter;
        private readonly IStoragePort _storageAdapter;

        // properties


        // constructors
        public AdministratorInteractor(IStoragePort storageAdapter, IPresenterPort presenterAdapter)
        {
            _storageAdapter = storageAdapter ?? throw new ArgumentNullException(nameof(storageAdapter));
            _presenterAdapter = presenterAdapter ?? throw new ArgumentNullException(nameof(presenterAdapter));
        }

        // methods
        public async Task<UseCaseResult<EvaluationPeriodModel>> AddNewEvaluationPeriodAsync(EvaluationPeriodModel evaluationPeriodModel) 
        {
            var useCase = new AddNewEvaluationPeriodUseCase(_storageAdapter, _presenterAdapter);
            var result = await useCase.RunAsync(evaluationPeriodModel);

            return result;
        }

        public async Task<UseCaseResult<EvaluateeModel>> AddNewEvaluationToEvaluateeAsync(uint employeeNumberOfEvaluatee, EvaluationPeriodModel evaluationPeriodModel)
        {
            var useCase = new AddNewEvaluationToEvaluateeUseCase(_storageAdapter, _presenterAdapter);
            var result = await useCase.RunAsync(employeeNumberOfEvaluatee, evaluationPeriodModel);

            return result;
        }
    }
}
