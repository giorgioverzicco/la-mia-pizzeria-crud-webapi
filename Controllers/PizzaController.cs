using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using la_mia_pizzeria_crud_webapi.Data;
using la_mia_pizzeria_crud_webapi.Interfaces;
using la_mia_pizzeria_crud_webapi.Models;
using la_mia_pizzeria_crud_webapi.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace la_mia_pizzeria_crud_webapi.Controllers;

[Authorize]
public class PizzaController : Controller
{
    private readonly ApplicationDbContext _ctx;
    private readonly IUnitOfWork _unitOfWork;
    
    public PizzaController(ApplicationDbContext ctx, IUnitOfWork unitOfWork)
    {
        _ctx = ctx;
        _unitOfWork = unitOfWork;
    }

    // GET
    public IActionResult Index()
    {
        var pizzas = 
            _unitOfWork.Pizza.GetAll("Category,Ingredients");
        return View(pizzas);
    }
    
    // GET: Pizza/Details/{id?}
    public IActionResult Details(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }
        
        var pizza = 
            _unitOfWork.Pizza
                .GetFirstOrDefault(
                    x => x.Id == id, 
                    "Category,Ingredients");

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
    public IActionResult Create(PizzaVm pizzaVm)
    {
        if (!ModelState.IsValid)
        {
            return View(pizzaVm);
        }

        var pizza = pizzaVm.Pizza;

        pizza.Ingredients =
            _unitOfWork.Ingredient
                .GetSelectedIngredients(pizzaVm.SelectedIngredients)
                .ToList();

        _unitOfWork.Pizza.Add(pizza);
        _unitOfWork.Save();
        
        return RedirectToAction(nameof(Index));
    }
    
    // GET: Pizza/Edit/{id?}
    public IActionResult Edit(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }
        
        var pizza = 
            _unitOfWork.Pizza
                .GetFirstOrDefault(
                    x => x.Id == id, 
                    "Ingredients");

        if (pizza is null)
        {
            return NotFound();
        }
        
        var pizzaVm = GetViewModelWithDropDownLists();
        pizzaVm.Pizza = pizza;
        pizzaVm.SelectedIngredients = pizza.Ingredients!.Select(x => x.Id);
        
        return View(pizzaVm);
    }
    
    // POST: Pizza/Edit/{id?}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(PizzaVm pizzaVm)
    {
        if (!ModelState.IsValid)
        {
            return View(pizzaVm);
        }
        
        var ingredients = 
            _unitOfWork.Ingredient
                .GetSelectedIngredients(pizzaVm.SelectedIngredients)
                .ToList();

        _unitOfWork.Pizza.Update(pizzaVm.Pizza, ingredients);
        _unitOfWork.Save();
        
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
        
        var pizza = _unitOfWork.Pizza.GetFirstOrDefault(x => x.Id == id);

        if (pizza is null)
        {
            return NotFound();
        }
        
        _unitOfWork.Pizza.Remove(pizza);
        _unitOfWork.Save();
        
        return RedirectToAction(nameof(Index));
    }

    private PizzaVm GetViewModelWithDropDownLists()
    {
        return new PizzaVm
        {
            Pizza = new Pizza(), 
            Categories = _unitOfWork.Category.GetSelectListItem(),
            Ingredients = _unitOfWork.Ingredient.GetSelectListItem()
        };
    }
}