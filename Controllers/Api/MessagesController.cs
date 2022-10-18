using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using la_mia_pizzeria_crud_webapi.Data;
using la_mia_pizzeria_crud_webapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_crud_webapi.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ApplicationDbContext _ctx;

        public MessagesController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var messages = _ctx.Messages.ToList();
            return Ok(messages);
        }
        
        [HttpGet]
        [Route("{id:int}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var message = _ctx.Messages.Find(id);

            if (message is null)
            {
                return NotFound();
            }
            
            return Ok(message);
        }

        [HttpPost]
        public IActionResult Post(Message message)
        {
            _ctx.Messages.Add(message);
            _ctx.SaveChanges();
            
            return CreatedAtAction(nameof(Get), new { id = message.Id }, message);
        }
    }
}
