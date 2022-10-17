using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using la_mia_pizzeria_crud_webapi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_crud_webapi.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public PizzasController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get(string? name)
        {
            var pizzas = _db.Pizzas.AsQueryable();
            
            if (name is not null)
            {
                pizzas = pizzas.Where(x => x.Name.Contains(name));
            }
            
            return Ok(pizzas.ToList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var pizza = _db.Pizzas.Find(id);

            if (pizza is null)
            {
                return NotFound();
            }

            return Ok(pizza);
        }
    }
}
