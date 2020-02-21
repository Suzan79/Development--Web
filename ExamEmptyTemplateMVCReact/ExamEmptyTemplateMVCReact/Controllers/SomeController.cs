using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SimpleModelsAndRelations.Model;

namespace EmptyTemplateMVCReact.Controllers
{
    public class SomeController : Controller
    {
        private readonly SomeContext _context;


        public SomeController(SomeContext context)
        {
          _context = context;
        }

        [HttpGet()]
        public IActionResult Get()
        {
            return Ok();
        }
      
    }
}