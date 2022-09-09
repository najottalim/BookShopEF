using System.Linq.Expressions;
using BookStore.Domain.Commons;

namespace BookStore.Data.IRepositories;

public interface IGenericRepository<TSource> where TSource : Auditable
{
    Task<TSource> CreateAsync(TSource source);
    Task<TSource> UpdateAsync(int id, TSource source);
    Task DeleteAsync(TSource source);
    
    Task<TSource?> GetAsync(Expression<Func<TSource, bool>> expression);
    IQueryable<TSource> GetAll(Expression<Func<TSource, bool>>? expression = null);
}