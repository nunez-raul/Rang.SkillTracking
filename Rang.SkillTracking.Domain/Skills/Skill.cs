using System;

namespace Rang.SkillTracking.Domain.Skills
{
    public class Skill : BaseEntity
    {
        // fields
        
        // properties
        public string Name { get; private set; }

        // constructors
        public Skill(string name)
            :base()
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        // methods
    }
}
