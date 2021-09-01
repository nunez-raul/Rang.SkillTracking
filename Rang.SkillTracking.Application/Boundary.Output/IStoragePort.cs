using Rang.SkillTracking.Domain.Skills;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Application.Boundary.Output
{
    public interface IStoragePort
    {
        // read operations
        Task<Evaluatee> GetEvaluateeByEmployeeNumberAsync(uint employeeNumber);
        Task<EvaluationPeriod> GetEvaluationPeriodByStartDateAsync(TimeZoneInfo targetTimeZoneInfo, DateTime startDate);
        Task<IEnumerable<EvaluationPeriod>> GetEvaluationPeriodsThatOverlapWithAsync(TimeZoneInfo targetTimeZoneInfo, EvaluationPeriod evaluationPeriod);

        // write operations
        Task<Evaluatee> SaveEvaluateeAsync(Evaluatee evaluatee);
        Task<EvaluationPeriod> AddNewEvaluationPeriodAsync(EvaluationPeriod evaluationPeriod);
    }
}
