namespace la_mia_pizzeria_crud_webapi.Interfaces;

public interface IUnitOfWork
{
    IPizzaRepository Pizza { get; }
    IMessageRepository Message { get; }
    IIngredientRepository Ingredient { get; }
    ICategoryRepository Category { get; }
    void Save();
}