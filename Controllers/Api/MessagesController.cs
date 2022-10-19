using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using la_mia_pizzeria_crud_webapi.Data;
using la_mia_pizzeria_crud_webapi.Interfaces;
using la_mia_pizzeria_crud_webapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_crud_webapi.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessagesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var messages = _unitOfWork.Message.GetAll();
            return Ok(messages);
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            var message = _unitOfWork.Message.GetFirstOrDefault(x => x.Id == id);

            if (message is null)
            {
                return NotFound();
            }
            
            return Ok(message);
        }

        [HttpPost]
        public IActionResult Post(Message message)
        {
            _unitOfWork.Message.Add(message);
            _unitOfWork.Save();
            
            return CreatedAtAction(nameof(Get), new { id = message.Id }, message);
        }
    }
}
