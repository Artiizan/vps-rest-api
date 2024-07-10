using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

using Models;
using Persistence;
using Helpers;

namespace Services;

public interface IGenericRepository<T> where T : class
{
    Task<PagedResponse<T>> GetAsync(QueryParameters queryParameters, string baseUrl, string[]? navigationProperties = null);
}

public class GenericRepository<T>(DatabaseContext db) : IGenericRepository<T> where T : class
{
    private readonly DatabaseContext _db = db;

    public async Task<PagedResponse<T>> GetAsync(
        QueryParameters queryParameters, string baseUrl, string[]? navigationProperties = null
    )
    {
        IQueryable<T> query = _db.Set<T>().AsQueryable();

        // Apply filtering
        if (!string.IsNullOrEmpty(queryParameters.filter))
        {
            List<FilterCriteria> filterCriteriaList = QueryParser.ParseFilterQuery(queryParameters.filter);
            if (filterCriteriaList.Count > 0)
            {
                foreach (var filterCriteria in filterCriteriaList)
                {
                    string dynamicFilter = $"{filterCriteria.Column} {filterCriteria.Operator} @0";
                    query = query.Where(dynamicFilter, filterCriteria.Value);
                }
            }
        }

        // Apply sorting
        if (!string.IsNullOrEmpty(queryParameters.sort))
        {
            string order = queryParameters.order.Equals("asc", StringComparison.OrdinalIgnoreCase) ? "" : " descending";
            string orderByExpression = $"{queryParameters.sort}{order}";
            query = query.OrderBy(orderByExpression);
        }

        int totalRecords = await query.CountAsync();

        // Apply pagination
        query = query.Skip((queryParameters.page - 1) * queryParameters.pageSize).Take(queryParameters.pageSize);

        // Include data from related tables
        if (navigationProperties != null)
        {
            query = navigationProperties.Aggregate(query, (currentQuery, navigationProperty) =>
                currentQuery.Include(navigationProperty));
        }

        // Pagination metadata
        List<T> data = await query.ToListAsync();
        int totalPages = (int)Math.Ceiling(totalRecords / (double)queryParameters.pageSize);

        PagedResponse<T> response = new(data, totalRecords, queryParameters.page, queryParameters.pageSize);

        // Constructing Links
        response._metadata.Links.Add("self", $"{baseUrl}?page={queryParameters.page}&per_page={queryParameters.pageSize}");
        response._metadata.Links.Add("first", $"{baseUrl}?page=1&per_page={queryParameters.pageSize}");
        response._metadata.Links.Add("previous", $"{baseUrl}?page={Math.Max(1, queryParameters.page - 1)}&per_page={queryParameters.pageSize}");
        response._metadata.Links.Add("next", $"{baseUrl}?page={Math.Min(totalPages, queryParameters.page + 1)}&per_page={queryParameters.pageSize}");
        response._metadata.Links.Add("last", $"{baseUrl}?page={totalPages}&per_page={queryParameters.pageSize}");

        return response;
    }
}