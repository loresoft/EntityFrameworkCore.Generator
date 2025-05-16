namespace EntityFrameworkCore.Generator.Metadata.Parsing;

public class CodeRegion
{
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
    public string Name { get; set; } = null!;
    public string? Content { get; set; }
}
