using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_crud_webapi.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public virtual ICollection<Pizza> Pizzas { get; set; }
}