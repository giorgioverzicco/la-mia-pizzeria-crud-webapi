using la_mia_pizzeria_crud_webapi.Data;
using la_mia_pizzeria_crud_webapi.Interfaces;
using la_mia_pizzeria_crud_webapi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_crud_webapi.Repositories;

public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
{
    private readonly ApplicationDbContext _db;
    
    public IngredientRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public IEnumerable<Ingredient> GetSelectedIngredients(IEnumerable<int> selectedIngredients)
    {
        IQueryable<Ingredient> query = _db.Ingredients;
        
        query = query.Where(x => selectedIngredients.Contains(x.Id));
        
        return query.ToList();
    }

    public IEnumerable<SelectListItem> GetSelectListItem()
    {
        return _db.Ingredients
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            })
            .ToList();
    }
}