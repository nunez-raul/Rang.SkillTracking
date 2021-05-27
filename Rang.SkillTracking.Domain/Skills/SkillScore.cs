using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rang.SkillTracking.Domain.Skills
{
    public class SkillScore
    {
        // fields
        protected List<string> _notes;

        // properties
        public SkillGoal SkillGoal { get; private set; }
        public int Score { get; set; }
        public IEnumerable<string> Notes { get => _notes; }

        // constructors
        public SkillScore(SkillGoal skillGoal)
        {
            SkillGoal = skillGoal ?? throw new ArgumentNullException(nameof(skillGoal));
            _notes = new List<string>();
        }

        // methods
        public void AddNote(string note)
        {
            _notes.Add(note);
        }
    }
}
