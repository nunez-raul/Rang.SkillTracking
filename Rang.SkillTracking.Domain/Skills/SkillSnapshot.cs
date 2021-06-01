using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Domain.Skills
{
    public class SkillSnapshot
    {
        // fields

        // properties
        public TrackingPoint TrackingPoint { get; private set; }
        public PersonalSkill PersonalSkill { get; private set; }
        public DateTime UtcTimestamp { get; private set; }

        // constructors
        public SkillSnapshot(TrackingPoint trackingPoint, PersonalSkill personalSkill)
        {
            TrackingPoint = trackingPoint ?? throw new ArgumentNullException(nameof(trackingPoint));
            PersonalSkill = personalSkill ?? throw new ArgumentNullException(nameof(personalSkill));
            UtcTimestamp = DateTime.UtcNow;
        }

        // methods

    }
}
