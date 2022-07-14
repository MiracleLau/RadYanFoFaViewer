using System.Collections.Generic;

namespace RadYanFoFaViewer.Models;

public class ApiResponseResult
{
    public bool Error { get; set; }
    public int Size { get; set; }
    public int Page { get; set; }
    public string? Mode { get; set; }
    public string? Query { get; set; }
    public int TotalPage { get; set; }
    public List<SearchResult>? Results { get; set; }
}