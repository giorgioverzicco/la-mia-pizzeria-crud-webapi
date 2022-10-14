using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace la_mia_pizzeria_crud_webapi.Models;

public class Pizza
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "{0} is required.")]
    [StringLength(30, ErrorMessage = "{0} must contain a maximum of {1} characters.")]
    public string Name { get; set; } = null!;
    
    [Required(ErrorMessage = "{0} is required.")]
    [RegularExpression(@"(\w+\s){4,}\w+", ErrorMessage = "{0} must contains at least 5 words.")]
    public string Description { get; set; } = null!;
    
    [Required(ErrorMessage = "{0} is required.")]
    [Url(ErrorMessage = "{0} is not a valid url.")]
    public string Photo { get; set; } = null!;
    
    [Required(ErrorMessage = "{0} is required.")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "money")]
    [Range(0, 100, ErrorMessage = "{0} must be between {1:C} and {2:C}.")]
    public decimal Price { get; set; }
    
    public int CategoryId { get; set; }
    [JsonIgnore]
    public Category? Category { get; set; }
    
    [JsonIgnore]
    public virtual ICollection<Ingredient> Ingredients { get; set; }
}