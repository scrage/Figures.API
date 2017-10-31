using System.Linq;

namespace Figures.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [Route("api/figures")]
    public class FiguresController : Controller
    {
        [HttpGet()]
        public IActionResult GetFigures()
        {
            return Ok(FiguresDataStore.Current.Figures);
        }

        [HttpGet("{id}")]

        public IActionResult GetFigure(int id)
        {
            var figureToReturn = FiguresDataStore.Current.Figures.FirstOrDefault(f => f.Id == id);

            if (figureToReturn == null)
            {
                return NotFound();
            }

            return Ok(figureToReturn);
        }
    }
}
