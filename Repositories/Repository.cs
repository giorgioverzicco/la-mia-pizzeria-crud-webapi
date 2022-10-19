using System.Linq.Expressions;
using la_mia_pizzeria_crud_webapi.Data;
using la_mia_pizzeria_crud_webapi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_crud_webapi.Repositories;

public class Repository<T> : IRepository<T>
    where T : class
{
    private readonly DbSet<T> _dbSet;

    protected Repository(ApplicationDbContext db)
    {
        _dbSet = db.Set<T>();
    }

    public IEnumerable<T> GetAll(string? includeRelations = null)
    {
        IQueryable<T> query = _dbSet;

        if (includeRelations is not null)
        {
            var relations = 
                includeRelations.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var relation in relations)
            {
                query = query.Include(relation);
            }
        }
        
        return query.ToList();
    }

    public T? GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeRelations = null)
    {
        IQueryable<T> query = _dbSet;
        
        if (includeRelations is not null)
        {
            var relations = 
                includeRelations.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var relation in relations)
            {
                query = query.Include(relation);
            }
        }
        
        return query.FirstOrDefault(filter);
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public bool Exists(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = _dbSet;
        return query.Any(filter);
    }
}