using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EntityFrameworkCore.Generator.Parsing
{
    public class RegionParser
    {
        private static readonly Lazy<Regex> _regionExpression = new Lazy<Regex>(() =>
            new Regex(@"(?:(?<end>^[ \t]*#endregion.*\r?\n)|(?<start>^[ \t]*#region(?:[ \t]+(?<name>[^\r\n]*))?\r?\n))", RegexOptions.Multiline | RegexOptions.Compiled));


        public Dictionary<string, CodeRegion> ParseRegions(string content)
        {
            var regions = new Dictionary<string, CodeRegion>(StringComparer.OrdinalIgnoreCase);
            var stack = new Stack<CodeRegion>();

            var matches = _regionExpression.Value.Matches(content);

            foreach (Match match in matches)
            {
                var isStart = match.Groups["start"].Success;
                if (isStart)
                {
                    var region = new CodeRegion
                    {
                        StartIndex = match.Index + match.Length,
                        Name = match.Groups["name"].Value.Trim()
                    };
                    stack.Push(region);
                }
                else
                {
                    var region = stack.Pop();
                    region.EndIndex = match.Index;
                    region.Content = content.Substring(region.StartIndex, region.EndIndex - region.StartIndex);
                    regions[region.Name] = region;
                }
            }

            return regions;
        }

    }
}
