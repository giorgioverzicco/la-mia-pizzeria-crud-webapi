using la_mia_pizzeria_crud_webapi.Models;

namespace la_mia_pizzeria_crud_webapi.Interfaces;

public interface IPizzaRepository : IRepository<Pizza>
{
    void Update(Pizza pizza, IEnumerable<Ingredient> ingredients);
}