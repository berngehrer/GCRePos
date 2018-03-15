using System;
using System.Text.RegularExpressions;

namespace GCRePos
{
    static class RegexHelper
    {
        public static string Replace(string line, string pattern, OffsetController offset)
        {
            var format = new DoubleFormatter();
            Func<Match, string> evaluate = (match) =>
            {
                if (TryParse(match, out double value)) {
                    return string.Format(format, "{0}{1}", match.Value[0], offset.Adjust(value));
                }
                return line;
            };

            var evaluator = new MatchEvaluator(evaluate);
            return Regex.Replace(line, pattern, evaluator);
        }


        public static bool TryParse(string line, string pattern, out double value)
        {
            var match = Regex.Match(line, pattern);
            return TryParse(match, out value);
        }

        public static bool TryParse(Match match, out double value)
        {
            value = 0.0d;
            if (match.Success) {
                var rawValue = match.Groups["value"].Value.Replace('.', ',');
                return double.TryParse(rawValue, out value);
            }
            return false;
        }
    }
}
