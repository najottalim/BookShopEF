using System.Linq.Expressions;
using BookStore.Data.DbContexts;
using BookStore.Data.IRepositories;
using BookStore.Domain.Commons;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Repositories;

public class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : Auditable
{
    protected readonly BookStoreDbContext _dbContext;
    protected readonly DbSet<TSource> _dbSet;
    
    public GenericRepository(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TSource>();
    }
    
    public async Task<TSource> CreateAsync(TSource source)
    {
        var entry = await _dbSet.AddAsync(source);
        
        return entry.Entity;
    }

    public Task<TSource> UpdateAsync(TSource source) 
        => Task.FromResult(_dbSet.Update(source).Entity);

    public void DeleteRange(IEnumerable<TSource> sources) 
        => _dbSet.RemoveRange(sources);

    public Task<TSource?> GetAsync(Expression<Func<TSource, bool>> expression)
        => _dbSet.FirstOrDefaultAsync(expression);

    public IQueryable<TSource> GetAll(Expression<Func<TSource, bool>>? expression = null)
        => expression is null ? _dbSet : _dbSet.Where(expression);
}