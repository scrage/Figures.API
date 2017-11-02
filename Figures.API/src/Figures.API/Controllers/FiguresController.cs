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
                FigureType = newFigure.FigureType,
                FirstName = newFigure.FirstName,
                LastName = newFigure.LastName,
                MiddleName = newFigure.MiddleName,
                Gender = newFigure.Gender,
                UniquelyDisplayedFullName = newFigure.UniquelyDisplayedFullName,
                IsLastNameFirst = newFigure.IsLastNameFirst,
                Alias = newFigure.Alias,
                Description = newFigure.Description,
                Title = newFigure.Title,
                CalculatedFullName = FieldProcessor.CalculateFullName(newFigure)
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

            // TODO: remove code duplication.
            figureFromStore.FigureType = updatedFigure.FigureType;
            figureFromStore.Gender = updatedFigure.Gender;
            figureFromStore.FirstName = updatedFigure.FirstName;
            figureFromStore.LastName = updatedFigure.LastName;
            figureFromStore.MiddleName = updatedFigure.MiddleName;
            figureFromStore.UniquelyDisplayedFullName = updatedFigure.UniquelyDisplayedFullName;
            figureFromStore.Title = updatedFigure.Title;
            figureFromStore.Alias = updatedFigure.Alias;
            figureFromStore.IsLastNameFirst = updatedFigure.IsLastNameFirst;
            figureFromStore.Description = updatedFigure.Description;
            figureFromStore.CalculatedFullName = FieldProcessor.CalculateFullName(updatedFigure);

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

            // TODO: remove code duplication.
            var figureToPatch = new FigureForUpdateDto()
            {
                FigureType = figureFromStore.FigureType,
                FirstName = figureFromStore.FirstName,
                LastName = figureFromStore.LastName,
                MiddleName = figureFromStore.MiddleName,
                Gender = figureFromStore.Gender,
                UniquelyDisplayedFullName = figureFromStore.UniquelyDisplayedFullName,
                IsLastNameFirst = figureFromStore.IsLastNameFirst,
                Alias = figureFromStore.Alias,
                Description = figureFromStore.Description,
                Title = figureFromStore.Title,
                CalculatedFullName = FieldProcessor.CalculateFullName(figureFromStore)
            };

            patchDoc.ApplyTo(figureToPatch, ModelState);

            TryValidateModel(figureToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // TODO: remove code duplication.
            figureFromStore.FigureType = figureToPatch.FigureType;
            figureFromStore.Gender = figureToPatch.Gender;
            figureFromStore.FirstName = figureToPatch.FirstName;
            figureFromStore.LastName = figureToPatch.LastName;
            figureFromStore.MiddleName = figureToPatch.MiddleName;
            figureFromStore.UniquelyDisplayedFullName = figureToPatch.UniquelyDisplayedFullName;
            figureFromStore.Title = figureToPatch.Title;
            figureFromStore.Alias = figureToPatch.Alias;
            figureFromStore.IsLastNameFirst = figureToPatch.IsLastNameFirst;
            figureFromStore.Description = figureToPatch.Description;
            figureFromStore.CalculatedFullName = FieldProcessor.CalculateFullName(figureToPatch);

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
