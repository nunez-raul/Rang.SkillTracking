using Rang.SkillTracking.Domain.Common;
using System;

namespace Rang.SkillTracking.Domain.Skills
{
    public class EvaluationPeriod : BaseEntity<EvaluationPeriodModel>
    {
        // fields    

        // properties
        public TimeZoneInfo TimeZoneInfo { get => _model.TimeZoneInfo; protected set=> _model.TimeZoneInfo = value; }
        public DateTime StartDate { get => _model.StartDate; protected set => _model.StartDate = value; }
        public DateTime EndDate { get => _model.EndDate; protected set => _model.EndDate = value; }

        public DateTime StartDateInUtc { get => DateTime.SpecifyKind(_model.StartDateInUtc, DateTimeKind.Utc); }
        public DateTime EndDateInUtc { get => DateTime.SpecifyKind(_model.EndDateInUtc, DateTimeKind.Utc); }

        // constructors
        public EvaluationPeriod(TimeZoneInfo timeZoneInfo, DateTime startDate, DateTime endDate)
            :base(new EvaluationPeriodModel { TimeZoneInfo = timeZoneInfo, StartDate = startDate, EndDate = endDate })
        {
            TimeZoneInfo = timeZoneInfo ?? throw new ArgumentNullException(nameof(timeZoneInfo));

            if (startDate >= endDate)
                throw new ApplicationException(string.Format("the supplied {0} should be bigger than the supplied {1}",nameof(endDate), nameof(startDate)));

            _model.StartDateInUtc = TimeZoneInfo.ConvertTimeToUtc(StartDate, TimeZoneInfo);
            _model.EndDateInUtc = TimeZoneInfo.ConvertTimeToUtc(EndDate, TimeZoneInfo);
        }

        public EvaluationPeriod(EvaluationPeriodModel evaluationPeriodModel, TimeZoneInfo targetTimeZoneInfo)
           : base(evaluationPeriodModel)
        {
            TimeZoneInfo = targetTimeZoneInfo ?? throw new ArgumentNullException(nameof(targetTimeZoneInfo));
            StartDate = TimeZoneInfo.ConvertTimeFromUtc(StartDateInUtc, TimeZoneInfo);
            EndDate = TimeZoneInfo.ConvertTimeFromUtc(EndDateInUtc, TimeZoneInfo);
        }

        // methods
        public override EvaluationPeriodModel GetModel()
        {
            return _model;
        }

        protected override void InitializeMe()
        {

        }

        protected override bool ValidateMe()
        {
            if (StartDateInUtc >= EndDateInUtc)
                return false;
            return true;
        }
    }
}
