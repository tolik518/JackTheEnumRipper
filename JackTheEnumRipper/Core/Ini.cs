using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace JackTheEnumRipper.Core
{
    public readonly record struct Setting
    {
        public string Comment { get; init; }

        public required string Name { get; init; }

        public required object Value { get; init; }
    }

    public readonly record struct Section
    {
        public string Comment { get; init; }

        public required string Name { get; init; }

        public required IEnumerable<Setting> Settings { get; init; }

        public bool IsGlobal { get; init; }
    }

    public class Ini
    {
        #region Private Fields

        private readonly List<Section> _sections = new List<Section>();

        #endregion

        public Ini(IEnumerable<Section>? sections = null, string? comment = null, char? commentSymbol = null, StringComparison? comparisonType = null)
        {
            this.Comment = comment ?? string.Empty;
            this.CommentSymbol = commentSymbol ?? ';';
            this.ComparisonType = comparisonType ?? StringComparison.InvariantCultureIgnoreCase;

            if (sections != null)
            {
                foreach (Section section in sections)
                {
                    this.AddSection(section);
                }
            }
        }

        #region Properties

        public string Comment { get; set; }

        public char CommentSymbol { get; private set; }

        public StringComparison ComparisonType { get; private set; }

        #endregion

        #region Methods

        public void AddSection(Section section)
        {
            if (this._sections.Exists(s => string.Equals(s.Name, section.Name, ComparisonType)))
                throw new ArgumentException($"Duplicated section: {section.Name}");

            this._sections.Add(section);
        }

        public Section? GetSection(string name)
        {
            return this._sections.First(section => string.Equals(name, section.Name, this.ComparisonType));
        }

        public bool RemoveSection(string name)
        {
            Section section = this._sections.FirstOrDefault(section => string.Equals(name, section.Name, this.ComparisonType));

            if (section == default) return false;

            this._sections.Remove(section);
            return true;
        }

        public IEnumerable<string> GetSections()
        {
            return this._sections.Select(section => section.Name);
        }

        private void Serialize(StringBuilder builder, Section section)
        {
            if (!section.IsGlobal)
            {
                if (!string.IsNullOrEmpty(section.Comment))
                    builder.AppendLine($"{this.CommentSymbol} {section.Comment}");

                builder.AppendLine($"[{section.Name}]");
            }

            foreach (Setting setting in section.Settings)
            {
                if (!string.IsNullOrEmpty(setting.Comment))
                    builder.AppendLine($"{this.CommentSymbol} {setting.Comment}");

                builder.AppendLine($"{setting.Name} = {setting.Value}");
            }

            builder.AppendLine(string.Empty);
        }

        public override string ToString()
        {
            StringBuilder builder = new();

            var sections = this._sections.OrderBy(x => x.IsGlobal);

            if (!string.IsNullOrEmpty(this.Comment))
                builder.AppendLine($"{this.CommentSymbol} {this.Comment} {Environment.NewLine}");

            foreach (Section section in sections)
            {
                this.Serialize(builder, section);
            }

            return builder.ToString();
        }

        #endregion
    }
}
