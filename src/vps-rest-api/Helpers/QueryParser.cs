using System.Text.RegularExpressions;

namespace Helpers;

public class FilterCriteria
{
    public string? Column { get; set; }
    public string? Operator { get; set; }
    public string? Value { get; set; }
}

public partial class QueryParser
{
    public static List<FilterCriteria> ParseFilterQuery(string filterQuery)
    {
        List<FilterCriteria> criteriaList = [];

        // Split the query by & to get individual filters
        string[] filters = filterQuery.Split('&', StringSplitOptions.RemoveEmptyEntries);

        foreach (string filter in filters)
        {
            // Use Regex to extract the column, operator, and value
            Match match = MyRegex().Match(filter);
            if (match.Success)
            {
                criteriaList.Add(new FilterCriteria
                {
                    Column = match.Groups[1].Value,
                    Operator = match.Groups[2].Value,
                    Value = match.Groups[3].Value
                });
            }
        }

        return criteriaList;
    }

    [GeneratedRegex(@"(.*?)\s*(=|!=|>|<|>=|<=)\s*(.*)")]
    private static partial Regex MyRegex();
}