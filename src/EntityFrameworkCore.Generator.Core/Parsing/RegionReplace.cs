using System.Text;

namespace EntityFrameworkCore.Generator.Parsing;

public class RegionReplace
{
    public RegionReplace(RegionParser regionParser = null)
    {
        RegionParser = regionParser ?? new RegionParser();
    }

    protected RegionParser RegionParser { get; }

    public void MergeFile(string fullPath, string outputContent)
    {
        var originalContent = File.ReadAllText(fullPath);

        var finalContent = MergeContent(originalContent, outputContent);

        File.WriteAllText(fullPath, finalContent);
    }

    public string MergeContent(string originalContent, string outputContent)
    {
        var outputRegions = RegionParser.ParseRegions(outputContent);

        var originalRegions = RegionParser.ParseRegions(originalContent);
        var originalBuilder = new StringBuilder(originalContent);

        int offset = 0;
        foreach (var pair in outputRegions)
        {
            var outputRegion = pair.Value;
            if (!originalRegions.TryGetValue(pair.Key, out var originalRegion))
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
