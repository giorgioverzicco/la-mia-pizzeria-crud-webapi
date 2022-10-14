using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_crud_webapi.Models;

public class PizzaViewModel
{
    public PizzaViewModel()
    {
        Categories = new List<SelectListItem>();
        IngredientIds = new List<int>();
        Ingredients = new List<SelectListItem>();
    }
    
    public Pizza Pizza { get; set; } = null!;
    public IEnumerable<SelectListItem> Categories { get; set; }
    public IEnumerable<int> IngredientIds { get; set; }
    public IEnumerable<SelectListItem> Ingredients { get; set; }
}