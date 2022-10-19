using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_crud_webapi.Models.ViewModels;

public class PizzaVm
{
    public PizzaVm()
    {
        Categories = new List<SelectListItem>();
        SelectedIngredients = new List<int>();
        Ingredients = new List<SelectListItem>();
    }
    
    public Pizza Pizza { get; set; } = null!;
    
    [ValidateNever]
    public IEnumerable<SelectListItem> Categories { get; set; }
    
    public IEnumerable<int> SelectedIngredients { get; set; }
    
    [ValidateNever]
    public IEnumerable<SelectListItem> Ingredients { get; set; }
}