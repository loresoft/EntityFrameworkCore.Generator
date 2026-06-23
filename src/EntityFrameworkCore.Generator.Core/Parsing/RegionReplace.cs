using System.Text;

namespace EntityFrameworkCore.Generator.Parsing;

public class RegionReplace
{
    public void MergeFile(string fullPath, string outputContent)
    {
        if (string.IsNullOrEmpty(fullPath) || string.IsNullOrEmpty(outputContent) || !Path.Exists(fullPath))
            return;

        var originalContent = File.ReadAllText(fullPath);

        var finalContent = MergeContent(originalContent, outputContent);

        File.WriteAllText(fullPath, finalContent);
    }

    public string MergeContent(string originalContent, string outputContent)
    {
        if (string.IsNullOrEmpty(originalContent) || string.IsNullOrEmpty(outputContent))
            return originalContent;

        var outputRegions = RegionParser.ParseRegions(outputContent);

        var originalRegions = RegionParser.ParseRegions(originalContent);
        var originalBuilder = new StringBuilder(originalContent);

        int offset = 0;
        foreach (var outputRegion in outputRegions)
        {
            var originalRegion = originalRegions
                .Find(r =>
                    string.Equals(r.RegionName, outputRegion.RegionName, StringComparison.OrdinalIgnoreCase)
                    && r.ClassName == outputRegion.ClassName
                );

            if (originalRegion == null)
            {
                // log error
                continue;
            }

            int startIndex = originalRegion.StartIndex + offset;
            int beforeReplace = originalBuilder.Length;
            int length = (originalRegion.EndIndex + offset) - startIndex;

            originalBuilder.Remove(startIndex, length);
            originalBuilder.Insert(startIndex, outputRegion.Content);

            int afterReplace = originalBuilder.Length;

            offset += (afterReplace - beforeReplace);
        }

        var finalContent = originalBuilder.ToString();

        if (originalContent == finalContent)
            return finalContent;

        return finalContent;
    }
}
