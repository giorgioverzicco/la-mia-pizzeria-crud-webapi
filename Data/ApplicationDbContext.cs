using Microsoft.EntityFrameworkCore;
using la_mia_pizzeria_crud_webapi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace la_mia_pizzeria_crud_webapi.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
        
    }

    public DbSet<Pizza> Pizzas { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Ingredient> Ingredients { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
}