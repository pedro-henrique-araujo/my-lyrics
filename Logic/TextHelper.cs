using System;
using System.Linq;

namespace MyLyrics.Logic
{
    public class TextHelper
    {
        private static readonly string _sectionSeparator = "\n\n";
        private static readonly string _lineSeparator = "\n";

        public static string RemoveRepeatedSections(string text)
        {
            if (text is null) return null;

            var sections = text.Split(_sectionSeparator).ToList();
            var nonRepeatedSections = sections.Distinct().ToList();
            var nonRepeatedSectionsAndLines = nonRepeatedSections.Select(DistinctLines);
            return string.Join(_sectionSeparator, nonRepeatedSectionsAndLines).Trim();
        }

        private static string DistinctLines(string section)
        {
            var lines = section.Split(_lineSeparator).ToList();
            var nonRepeatedLines = lines.Distinct().ToList();
            return string.Join(_lineSeparator, nonRepeatedLines).Trim();
        }
    }
}