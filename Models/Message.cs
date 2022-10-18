using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_crud_webapi.Models;

public class Message
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = null!;
    
    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = null!;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    
    [Required]
    public string Content { get; set; } = null!;
}