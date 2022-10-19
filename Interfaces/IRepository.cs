using System.Linq.Expressions;

namespace la_mia_pizzeria_crud_webapi.Interfaces;

public interface IRepository<T> 
    where T : class
{
    IEnumerable<T> GetAll(string? includeRelations = null);
    T? GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeRelations = null);
    void Add(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}