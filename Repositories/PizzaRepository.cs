using la_mia_pizzeria_crud_webapi.Data;
using la_mia_pizzeria_crud_webapi.Interfaces;
using la_mia_pizzeria_crud_webapi.Models;

namespace la_mia_pizzeria_crud_webapi.Repositories;

public class PizzaRepository : Repository<Pizza>, IPizzaRepository
{
    private readonly ApplicationDbContext _db;
    
    public PizzaRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Pizza pizza, IEnumerable<Ingredient> ingredients)
    {
        var oldPizza = GetFirstOrDefault(x => x.Id == pizza.Id, "Ingredients")!;
        
        oldPizza.Name = pizza.Name;
        oldPizza.Description = pizza.Description;
        oldPizza.Photo = pizza.Photo;
        oldPizza.Price = pizza.Price;
        oldPizza.CategoryId = pizza.CategoryId;
        oldPizza.Ingredients = ingredients.ToList();
        
        _db.Pizzas.Update(oldPizza);
    }
}