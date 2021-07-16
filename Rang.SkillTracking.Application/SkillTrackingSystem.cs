using Rang.SkillTracking.Domain.Skills;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rang.SkillTracking.Application
{
    public class SkillTrackingSystem
    {
        // fields
        protected List<Skill> _skills;

        // properties
        public IEnumerable<Skill> Skills { get => _skills; }

        // constructors
        public SkillTrackingSystem()
        {
            _skills = new List<Skill>();
        }

        // methods
        public IEnumerable<Skill> AddNewSkills(string[] skillNames)
        {
            if (skillNames == null)
                throw new ArgumentNullException(nameof(skillNames));

            var skills = skillNames
                .Select(sn => new Skill(sn));

            _skills.AddRange(skills);

            return _skills;
        }
    }
}
