namespace Models;

public class ResponseMetadata(int totalRecords, int page, int pageSize)
{
    public int TotalRecords { get; set; } = totalRecords;
    public int Page { get; set; } = page;
    public int PageSize { get; set; } = pageSize;
    public int PageCount { get; set; } = (int)Math.Ceiling(totalRecords / (double)pageSize);
    public Dictionary<string, string> Links { get; set; } = [];
}

public class PagedResponse<T>(IEnumerable<T> data, int totalRecords, int page, int pageSize)
{
    public ResponseMetadata _metadata { get; set; } = new ResponseMetadata(totalRecords, page, pageSize);
    public IEnumerable<T> records { get; set; } = data;
}