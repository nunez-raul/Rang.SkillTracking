﻿using Microsoft.EntityFrameworkCore;
using Rang.SkillTracking.Application.Boundary.Output;
using Rang.SkillTracking.Application.Common;
using Rang.SkillTracking.Domain.Skills;
using Rang.SkillTracking.Persistence.Ef.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Persistence
{
    public class StorageAdapter: IStoragePort
    {
        // fields
        protected SkillTrackingDbContext _skillTrackingDbContext;

        // properties

        // constructors
        public StorageAdapter(SkillTrackingDbContext skillTrackingDbContext)
        {
            _skillTrackingDbContext = skillTrackingDbContext ?? throw new ArgumentNullException(nameof(skillTrackingDbContext));
        }

        // methods
        public async Task<Evaluatee> GetEvaluateeByEmployeeNumberAsync(uint employeeNumber)
        {
            var evaluateeModel = await _skillTrackingDbContext.EvaluateeModelSet
                .Where(evaluatee => evaluatee.EmployeeNumber == employeeNumber)
                .SingleOrDefaultAsync();

            if (evaluateeModel == null)
                return null;
            
            return new Evaluatee(evaluateeModel);
        }

        public async Task<EvaluationPeriod> GetEvaluationPeriodByStartDateAsync(TimeZoneInfo targetTimeZoneInfo, DateTime startDate)
        {
            var startDateInUtc = startDate.FromTimeZoneTimeToUtc(targetTimeZoneInfo);

            var evaluationPeriodInUtcDataModel = await _skillTrackingDbContext.EvaluationPeriodInUctModelSet
                .Where(period => period.StartDateInUtc == startDateInUtc)
                .SingleOrDefaultAsync();

            if (evaluationPeriodInUtcDataModel == null)
                return null;

            return new EvaluationPeriod(evaluationPeriodInUtcDataModel, targetTimeZoneInfo);
        }

        public async Task<IEnumerable<EvaluationPeriod>> GetEvaluationPeriodsThatOverlapWithAsync(TimeZoneInfo targetTimeZoneInfo, EvaluationPeriod evaluationPeriod)
        {
               var overlappingPeriods = await _skillTrackingDbContext.EvaluationPeriodInUctModelSet
                .Where(existingPeriod => existingPeriod.StartDateInUtc < evaluationPeriod.EndDateInUtc && existingPeriod.EndDateInUtc > evaluationPeriod.StartDateInUtc)
                .ToListAsync();

            return overlappingPeriods.Select(opm => new EvaluationPeriod(opm, targetTimeZoneInfo)).ToList();
        }

        public async Task<Evaluatee>  SaveEvaluateeAsync(Evaluatee evaluatee)
        {
            var evaluateeModel =  evaluatee.GetModel();


            var entityEntry = _skillTrackingDbContext.Entry(evaluateeModel);

            _skillTrackingDbContext.Attach(evaluateeModel);
            _skillTrackingDbContext.Entry(evaluateeModel).State = EntityState.Modified;
            await _skillTrackingDbContext.SaveChangesAsync()
                .ConfigureAwait(false);

            return evaluatee;
        }

        public async Task<EvaluationPeriod> AddNewEvaluationPeriodAsync(EvaluationPeriod evaluationPeriod)
        {
            var evaluationPeriodModel = evaluationPeriod.GetModel();

            await _skillTrackingDbContext.EvaluationPeriodInUctModelSet.AddAsync(evaluationPeriodModel);
            await _skillTrackingDbContext.SaveChangesAsync()
                .ConfigureAwait(false);

            return evaluationPeriod;
        }
    }
}
