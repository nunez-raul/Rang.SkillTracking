using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Domain.Skills
{
    public class Skill
    {
        // fields
        
        // properties
        public string Name { get; private set; }

        // constructors
        public Skill(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        // methods
    }
}
