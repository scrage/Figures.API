using System.Linq;
using Figures.API.Models;
using Microsoft.AspNetCore.JsonPatch;

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

        [HttpGet("{id}", Name= "GetFigure")]
        public IActionResult GetFigure(int id)
        {
            var figureToReturn = FiguresDataStore.Current.Figures.FirstOrDefault(f => f.Id == id);

            if (figureToReturn == null)
            {
                return NotFound();
            }

            return Ok(figureToReturn);
        }

        [HttpPost()]
        public IActionResult CreateFigure([FromBody] FigureForCreationDto newFigure)
        {
            if (newFigure == null)
            {
                return BadRequest();
            }

            var maxFigureId = FiguresDataStore.Current.Figures.Max(f => f.Id);

            var figureToCreate = new FigureDto()
            {
                Id = ++maxFigureId,
                Name = newFigure.Name,
                Gender = newFigure.Gender
            };

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            FiguresDataStore.Current.Figures.Add(figureToCreate);

            return CreatedAtRoute("GetFigure", figureToCreate.Id, figureToCreate);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFigure(int id, [FromBody] FigureForUpdateDto updatedFigure)
        {
            if (updatedFigure == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var figureFromStore = FiguresDataStore.Current.Figures.FirstOrDefault(f => f.Id == id);

            if (figureFromStore == null)
            {
                return NotFound();
            }

            figureFromStore.Name = updatedFigure.Name;
            figureFromStore.Gender = updatedFigure.Gender;

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateFigure(int id, [FromBody] JsonPatchDocument<FigureForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var figureFromStore = FiguresDataStore.Current.Figures.FirstOrDefault(f => f.Id == id);

            if (figureFromStore == null)
            {
                return NotFound();
            }

            var figureToPatch = new FigureForUpdateDto()
            {
                Name = figureFromStore.Name,
                Gender = figureFromStore.Gender
            };

            patchDoc.ApplyTo(figureToPatch, ModelState);

            TryValidateModel(figureToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            figureFromStore.Name = figureToPatch.Name;
            figureFromStore.Gender = figureToPatch.Gender;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFigure(int id)
        {
            var figureFromStore = FiguresDataStore.Current.Figures.FirstOrDefault(f => f.Id == id);

            if (figureFromStore == null)
            {
                return NotFound();
            }

            FiguresDataStore.Current.Figures.Remove(figureFromStore);

            return NoContent();
        }
    }
}
