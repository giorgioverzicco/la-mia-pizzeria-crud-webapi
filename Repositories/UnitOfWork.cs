using la_mia_pizzeria_crud_webapi.Data;
using la_mia_pizzeria_crud_webapi.Interfaces;

namespace la_mia_pizzeria_crud_webapi.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Pizza = new PizzaRepository(_db);
        Message = new MessageRepository(_db);
        Ingredient = new IngredientRepository(_db);
        Category = new CategoryRepository(_db);
    }

    public IPizzaRepository Pizza { get; }
    public IMessageRepository Message { get; }
    public IIngredientRepository Ingredient { get; }
    public ICategoryRepository Category { get; }
    
    public void Save()
    {
        _db.SaveChanges();
    }
}