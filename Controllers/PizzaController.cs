using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using la_mia_pizzeria_crud_webapi.Data;
using la_mia_pizzeria_crud_webapi.Models;
using Microsoft.AspNetCore.Authorization;

namespace la_mia_pizzeria_crud_webapi.Controllers;

[Authorize]
public class PizzaController : Controller
{
    private readonly ApplicationDbContext _ctx;
    
    public PizzaController(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    // GET
    public IActionResult Index()
    {
        List<Pizza> pizzas = 
            _ctx.Pizzas
                .Include(x => x.Category)
                .Include(x => x.Ingredients)
                .ToList();
        return View(pizzas);
    }
    
    // GET: Pizza/Details/{id?}
    public IActionResult Details(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }

        Pizza? pizza = 
            _ctx.Pizzas
                .Include(x => x.Category)
                .Include(x => x.Ingredients)
                .FirstOrDefault(x => x.Id == id);

        if (pizza is null)
        {
            return NotFound();
        }
        
        return View(pizza);
    }
    
    // GET: Pizza/Create
    public IActionResult Create()
    {
        var pizzaVm = GetViewModelWithDropDownLists();
        return View(pizzaVm);
    }
    
    // POST: Pizza/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(PizzaViewModel pizzaVm)
    {
        if (!ModelState.IsValid)
        {
            return View(pizzaVm);
        }

        var pizza = pizzaVm.Pizza;
        
        pizza.Ingredients = 
            _ctx.Ingredients
                .Where(x => pizzaVm.IngredientIds.Contains(x.Id))
                .ToList();

        _ctx.Pizzas.Add(pizza);
        _ctx.SaveChanges();
        
        return RedirectToAction(nameof(Index));
    }
    
    // GET: Pizza/Edit/{id?}
    public IActionResult Edit(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }

        Pizza? pizza = 
            _ctx.Pizzas
                .Include(x => x.Ingredients)
                .FirstOrDefault(x => x.Id == id);

        if (pizza is null)
        {
            return NotFound();
        }
        
        var pizzaVm = GetViewModelWithDropDownLists();
        pizzaVm.Pizza = pizza;
        pizzaVm.IngredientIds = pizza.Ingredients!.Select(x => x.Id);
        
        return View(pizzaVm);
    }
    
    // POST: Pizza/Edit/{id?}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(PizzaViewModel pizzaVm)
    {
        if (!ModelState.IsValid)
        {
            return View(pizzaVm);
        }

        var pizza =
            _ctx.Pizzas
                .Include(x => x.Ingredients)
                .FirstOrDefault(x => x.Id == pizzaVm.Pizza.Id)!;
        
        _ctx.Entry(pizza).State = EntityState.Detached;
        
        pizza = pizzaVm.Pizza;
        pizza.Ingredients = 
            _ctx.Ingredients
                .Where(x => pizzaVm.IngredientIds.Contains(x.Id))
                .ToList();

        _ctx.Pizzas.Update(pizza);
        _ctx.SaveChanges();
        
        return RedirectToAction(nameof(Index));
    }
    
    // POST: Pizza/Delete/{id?}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }
        
        Pizza? pizza = _ctx.Pizzas.Find(id);

        if (pizza is null)
        {
            return NotFound();
        }
        
        _ctx.Pizzas.Remove(pizza);
        _ctx.SaveChanges();
        
        return RedirectToAction(nameof(Index));
    }

    private PizzaViewModel GetViewModelWithDropDownLists()
    {
        var categories =
            _ctx.Categories
                .Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    })
                .ToList();
        
        var ingredients =
            _ctx.Ingredients
                .Select(x =>
                    new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    })
                .ToList();
        
        var pizzaVm = new PizzaViewModel
        {
            Pizza = new Pizza(), 
            Categories = categories,
            Ingredients = ingredients
        };

        return pizzaVm;
    }
}