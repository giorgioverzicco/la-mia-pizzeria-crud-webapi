using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using la_mia_pizzeria_crud_webapi.Data;
using la_mia_pizzeria_crud_webapi.Interfaces;
using la_mia_pizzeria_crud_webapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_crud_webapi.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PizzasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get(string? name)
        {
            var pizzas = _unitOfWork.Pizza.GetAll();
            
            if (name is not null)
            {
                pizzas = pizzas.Where(x => x.Name.ToLower().Contains(name.ToLower()));
            }
            
            return Ok(pizzas.ToList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            var pizza = _unitOfWork.Pizza.GetFirstOrDefault(x => x.Id == id);

            if (pizza is null)
            {
                return NotFound();
            }

            return Ok(pizza);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Put(int id, Pizza pizza)
        {
            if (id != pizza.Id)
            {
                return BadRequest();
            }
            
            if (!_unitOfWork.Pizza.Exists(x => x.Id == id))
            {
                return NotFound();
            }
            
            _unitOfWork.Pizza.Update(pizza, Enumerable.Empty<Ingredient>());
            _unitOfWork.Save();

            return NoContent();
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            var pizza = _unitOfWork.Pizza.GetFirstOrDefault(x => x.Id == id);
            
            if (pizza is null)
            {
                return NotFound();
            }

            _unitOfWork.Pizza.Remove(pizza);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
