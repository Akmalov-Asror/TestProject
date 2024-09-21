using EcommerceApi.V1.Commons.AuditableModel;
using System.Linq.Expressions;

namespace EcommerceApi.V1.Commons.GenericRepositories;

public interface IGenericRepository<T> where T : BaseEntity
{
    ValueTask<T> CreateAsync(T entity);
    IQueryable<T> GetAll(Expression<Func<T, bool>> expression, string[] includes = null,bool isTracking = true);
    ValueTask<T> GetAsync(Expression<Func<T, bool>> expression, bool istracking = true, string[] includes = null);
    T UpdateAsync(T entity);//homework
    ValueTask<bool> DeleteAsync(int id);
    ValueTask SaveChangesAsync();
}





