using BookStore.Domain.Commons;

namespace BookStore.Service.Extensions;

public static class CollectionExtensions
{
    public static IEnumerable<TSource> ToPaged<TSource>(this IQueryable<TSource> sources,
        PaginationParameters? parameters) 
        => parameters is null
            ? sources
            : sources.Skip(parameters.Skip).Take(parameters.Take);

    public static IEnumerable<TSource> ToPaged<TSource>(this IEnumerable<TSource> sources,
        PaginationParameters? parameters)
        => parameters is null
            ? sources
            : sources.Skip(parameters.Skip).Take(parameters.Take);
}