namespace EntityFrameworkCore.Generator.Metadata.Parsing;

public class CodeRegion
{
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
    public string RegionName { get; set; } = null!;
    public string? ClassName { get; set; }
    public string? Content { get; set; }
}
