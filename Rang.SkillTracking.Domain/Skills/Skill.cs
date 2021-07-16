using Rang.SkillTracking.Domain.Common;
using System;
using System.Collections.Generic;

namespace Rang.SkillTracking.Domain.Skills
{
    public class Skill : BaseEntity<SkillModel>
    {
        // fields
        private IList<Tag> _tags;

        // properties
        public string Name { get => _model.Name; private set => _model.Name = value; }
        public IEnumerable<Tag> Tags { get => _tags; }

        // constructors
        public Skill(string name)
            :base(new SkillModel { Name = name })
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

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

        public override SkillModel GetModel()
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
