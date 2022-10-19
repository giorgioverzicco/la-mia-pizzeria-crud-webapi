using la_mia_pizzeria_crud_webapi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_crud_webapi.Interfaces;

public interface IIngredientRepository : IRepository<Ingredient>
{
    IEnumerable<Ingredient> GetSelectedIngredients(IEnumerable<int> selectedIngredients);
    IEnumerable<SelectListItem> GetSelectListItem();
}