using Rang.SkillTracking.Application.Common;
using Rang.SkillTracking.Domain.Skills;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Application.Boundary.Input
{
    public interface IAdministratorPort
    {
        public Task<UseCaseResult<EvaluateeModel>> AddNewEvaluationToEvaluateeAsync(uint employeeNumberOfEvaluatee, EvaluationPeriodModel evaluationPeriodModel);
    }
}
