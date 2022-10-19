using la_mia_pizzeria_crud_webapi.Data;
using la_mia_pizzeria_crud_webapi.Interfaces;
using la_mia_pizzeria_crud_webapi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_crud_webapi.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _db;
    
    public CategoryRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public IEnumerable<SelectListItem> GetSelectListItem()
    {
        return _db.Categories
            .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            })
            .ToList();
    }
}