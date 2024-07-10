
namespace Models;

// TODO: Further improvements could be made to reduce database and network load by adding in projection queries
// to only return the columns that are needed. Especially for tables with many relationships.

public class QueryParameters
{
    // Pagination
    public int page { get; set; } = 1;
    public int pageSize { get; set; } = 50;

    // Filtering
    public string? filter { get; set; }

    // Sorting
    public string? sort { get; set; }
    public string order { get; set; } = "asc";
}