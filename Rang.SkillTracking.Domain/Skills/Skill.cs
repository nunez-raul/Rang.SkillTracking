using Rang.SkillTracking.Domain.Common;
using System;
using System.Collections.Generic;

namespace Rang.SkillTracking.Domain.Skills
{
    public class Skill : BaseEntity
    {
        // fields
        private IList<Tag> _tags;

        // properties
        public string Name { get; private set; }
        public IEnumerable<Tag> Tags { get => _tags; }

        // constructors
        public Skill(string name)
            :base()
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
            _tags = new List<Tag>();
        }

        // methods
        public Tag AddNewTag(string text)
        {
            var tag = new Tag(text);
            _tags.Add(tag);
            return tag;
        }

        public void AddTag(Tag tag)
        {
            _tags.Add(tag);
        }
    }
}
