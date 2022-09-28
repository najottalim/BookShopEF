using BookStore.Domain.Commons;

namespace BookStore.Service.Extensions;

public static class CollectionExtensions
{
    public static IEnumerable<TSource> ToPaged<TSource>(this IQueryable<TSource> sources,
        PaginationParameters? parameters)
        => parameters is {PageSize: > 0, PageIndex: > 0}
            ? sources.Skip((parameters.PageIndex - 1) * parameters.PageSize).Take(parameters.PageSize!)
            : sources;

    public static IEnumerable<TSource> ToPaged<TSource>(this IEnumerable<TSource> sources,
        PaginationParameters? parameters)
        => parameters is {PageSize: > 0, PageIndex: > 0}
            ? sources.Skip((parameters.PageIndex - 1) * parameters.PageSize).Take(parameters.PageSize!)
            : sources;
}