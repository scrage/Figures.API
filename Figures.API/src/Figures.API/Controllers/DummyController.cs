using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Figures.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Figures.API.Controllers
{
    public class DummyController : Controller
    {
        private FigureContext _ctx;

        public DummyController(FigureContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("api/testdatabase")]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}
