using System;

namespace Rang.SkillTracking.Domain.Skills
{
    public class EvaluationPeriod : BaseEntity
    {
        // fields
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;
        private readonly DateTime _startDateTimeInUtc;
        private readonly DateTime _endDateTimeInUtc;

        // properties
        public TimeZoneInfo TimeZoneInfo { get; protected set; }
        public DateTime StartDate { get => _startDate; }
        public DateTime EndDate { get => _endDate; }

        public DateTime StartDateInUtc { get => DateTime.SpecifyKind(_startDateTimeInUtc, DateTimeKind.Utc); }
        public DateTime EndDateInUtc { get => DateTime.SpecifyKind(_endDateTimeInUtc, DateTimeKind.Utc); }

        // constructors
        public EvaluationPeriod(TimeZoneInfo timeZoneInfo, DateTime startDate, DateTime endDate)
        {
            TimeZoneInfo = timeZoneInfo ?? throw new ArgumentNullException(nameof(timeZoneInfo));

            if (startDate >= endDate)
                throw new ApplicationException(string.Format("the supplied {0} should be bigger than the supplied {1}",nameof(endDate), nameof(startDate)));

            _startDate = startDate;
            _endDate = endDate;

            _startDateTimeInUtc = TimeZoneInfo.ConvertTimeToUtc(_startDate, TimeZoneInfo);
            _endDateTimeInUtc = TimeZoneInfo.ConvertTimeToUtc(_endDate, TimeZoneInfo);
        }

        // methods
    }
}
