using EcommerceApi.V1.Commons.AuditableModel;
using EcommerceApi.V1.Commons.EcommerceContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EcommerceApi.V1.Commons.GenericRepositories;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly EcommerceDbContext _context;
    protected readonly DbSet<T> dbSet;
    protected GenericRepository(EcommerceDbContext context)
    {
        this._context = context;
        this.dbSet = context.Set<T>();
    }
    public async ValueTask<T> CreateAsync(T entity) 
        => (await _context.AddAsync(entity)).Entity;

    public async ValueTask<bool> DeleteAsync(int id)
    {
        var entity = await GetAsync(x => x.Id == id);
        if (entity == null)
            return false;

        dbSet.Remove(entity);

        return true;
        
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> expression, string[] includes = null, bool isTracking = true)
    {
        var query = expression is null ? dbSet : dbSet.Where(expression);
        if(includes != null)
            foreach (var include in includes)
                if(!string.IsNullOrEmpty(include))
                    query = query.Include(include);

        if (!isTracking)
            query = query
                .AsTracking()
                .AsSplitQuery();

        return query;
    }

    public virtual async ValueTask<T> GetAsync(Expression<Func<T, bool>> expression, bool istracking = true, string[] includes = null) 
        => await GetAll(expression, includes, istracking).FirstOrDefaultAsync();


    public async ValueTask SaveChangesAsync() 
        => await _context.SaveChangesAsync();

    public T UpdateAsync(T entity) 
       => dbSet.Update(entity).Entity;
}


