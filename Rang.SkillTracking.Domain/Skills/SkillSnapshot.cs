using Rang.SkillTracking.Domain.Common;
using System;

namespace Rang.SkillTracking.Domain.Skills
{
    public class SkillSnapshot : BaseEntity<SkillSnapshotModel>
    {
        // fields

        // properties
        public TrackingPoint TrackingPoint { get; private set; }
        public PersonalSkill PersonalSkill { get; private set; }
        public DateTime UtcTimestamp { get; private set; }

        // constructors
        public SkillSnapshot(TrackingPoint trackingPoint, PersonalSkill personalSkill)
            :base()
        {
            TrackingPoint = trackingPoint ?? throw new ArgumentNullException(nameof(trackingPoint));
            PersonalSkill = personalSkill ?? throw new ArgumentNullException(nameof(personalSkill));
            UtcTimestamp = DateTime.UtcNow;
        }

        // methods
        public override SkillSnapshotModel GetModel()
        {
            return _model;
        }

        protected override void InitializeMe()
        {

        }

        protected override bool ValidateMe()
        {
            return true;
        }
    }
}
