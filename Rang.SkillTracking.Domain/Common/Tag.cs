using System;

namespace Rang.SkillTracking.Domain.Common
{
    public struct Tag
    {
        // fields
        private string _text;

        // properties
        public string Text { get => _text; }

        // constructor
        public Tag(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text));

            _text = text;
        }
    }
}
