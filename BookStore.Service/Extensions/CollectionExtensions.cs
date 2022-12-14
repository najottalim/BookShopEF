using BookStore.Domain.Commons;
using BookStore.Service.Helpers;

namespace BookStore.Service.Extensions;

public static class CollectionExtensions
{
    public static IEnumerable<TSource> ToPagedAsEnumerable<TSource>(this IQueryable<TSource> sources,
        PaginationParameters? parameters)
    {
        if (HttpContextHelper.ResponseHeaders.ContainsKey("total-count"))
            HttpContextHelper.ResponseHeaders.Remove("total-count");
            
        HttpContextHelper.ResponseHeaders.Add("total-count", $"{sources.Count()}");

        return parameters is {PageSize: > 0, PageIndex: > 0}
            ? sources.Skip((parameters.PageIndex - 1) * parameters.PageSize).Take(parameters.PageSize)
            : sources;
    }
    
    public static IQueryable<TSource> ToPagedAsQueryable<TSource>(this IQueryable<TSource> sources,
        PaginationParameters? parameters)
    {
        if (HttpContextHelper.ResponseHeaders.ContainsKey("total-count"))
            HttpContextHelper.ResponseHeaders.Remove("total-count");
            
        HttpContextHelper.ResponseHeaders.Add("total-count", $"{sources.Count()}");

        return parameters is {PageSize: > 0, PageIndex: > 0}
            ? sources.Skip((parameters.PageIndex - 1) * parameters.PageSize).Take(parameters.PageSize)
            : sources;
    }

    public static IEnumerable<TSource> ToPagedAsEnumerable<TSource>(this IEnumerable<TSource> sources,
        PaginationParameters? parameters)
        => parameters is {PageSize: > 0, PageIndex: > 0}
            ? sources.Skip((parameters.PageIndex - 1) * parameters.PageSize).Take(parameters.PageSize)
            : sources;
}