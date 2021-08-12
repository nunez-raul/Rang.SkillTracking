using Rang.SkillTracking.Application.Common;
using Rang.SkillTracking.Domain.Skills;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Application.Boundary.Input
{
    public interface IAdministratorPort
    {
        public Task<UseCaseResult<EvaluationPeriodModel>> AddNewEvaluationPeriodAsync(EvaluationPeriodModel evaluationPeriodModel);
        public Task<UseCaseResult<EvaluateeModel>> AddNewEvaluationToEvaluateeAsync(uint employeeNumberOfEvaluatee, EvaluationPeriodModel evaluationPeriodModel);
    }
}
