using System;
using System.Text;

namespace Figures.API.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Figures.API.Models;

    [Route("api/figures")]
    public class FiguresController : Controller
    {
        private ILogger<FiguresController> _logger;

        public FiguresController(ILogger<FiguresController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult GetFigures()
        {
            return Ok(FiguresDataStore.Current.Figures);
        }

        [HttpGet("{id}", Name = "GetFigure")]
        public IActionResult GetFigure(int id)
        {
            try
            {
                var figureToReturn = FiguresDataStore.Current.Figures.FirstOrDefault(f => f.Id == id);

                if (figureToReturn == null)
                {
                    _logger.LogInformation($"Figure with id {id} wasn't found.");
                    return NotFound();
                }

                return Ok(figureToReturn);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"Exception occurred while getting figure with id {id}", e);
                return StatusCode(500, "A problem has happened while handling your request.");
            }
        }

        [HttpPost()]
        public IActionResult CreateFigure([FromBody] FigureForCreationDto newFigure)
        {
            try
            {
                if (newFigure == null)
                {
                    _logger.LogInformation("New figure to insert cannot be null.");
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

                TryValidateModel(figureToCreate);

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"New figure to insert is invalid: {ModelState.ValidationState}");
                    return BadRequest();
                }

                FiguresDataStore.Current.Figures.Add(figureToCreate);
                return CreatedAtRoute("GetFigure", figureToCreate.Id, figureToCreate);
            }
            catch (Exception e)
            {
                _logger.LogCritical("Exception occurred while inserting new figure.", e);
                return StatusCode(500, "A problem has happened while handling your request.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFigure(int id, [FromBody] FigureForUpdateDto updatedFigure)
        {
            try
            {
                if (updatedFigure == null)
                {
                    _logger.LogInformation("Figure to update cannot be null.");
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Figure to update is invalid: {ModelState.ValidationState}");
                    return BadRequest();
                }

                var figureFromStore = FiguresDataStore.Current.Figures.FirstOrDefault(f => f.Id == id);

                if (figureFromStore == null)
                {
                    _logger.LogInformation($"Figure to update with id {id} wasn't found.");
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
            catch (Exception e)
            {
                _logger.LogCritical("Exception occurred while updating a figure.", e);
                return StatusCode(500, "A problem has happened while handling your request.");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateFigure(int id, [FromBody] JsonPatchDocument<FigureForUpdateDto> patchDoc)
        {
            try
            {
                if (patchDoc == null)
                {
                    _logger.LogInformation("Patch document cannot be null.");
                    return BadRequest();
                }

                var figureFromStore = FiguresDataStore.Current.Figures.FirstOrDefault(f => f.Id == id);

                if (figureFromStore == null)
                {
                    _logger.LogInformation($"Figure to patch with id {id} wasn't found.");
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
                    _logger.LogInformation($"Figure to patch is invalid: {ModelState.ValidationState}");
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
            catch (Exception e)
            {
                _logger.LogCritical("Exception occurred while patching a figure.", e);
                return StatusCode(500, "A problem has happened while handling your request.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFigure(int id)
        {
            try
            {
                var figureFromStore = FiguresDataStore.Current.Figures.FirstOrDefault(f => f.Id == id);

                if (figureFromStore == null)
                {
                    _logger.LogInformation($"Figure to delete with id {id} wasn't found.");
                    return NotFound();
                }

                FiguresDataStore.Current.Figures.Remove(figureFromStore);

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogCritical("Exception occurred while deleting a figure.", e);
                return StatusCode(500, "A problem has happened while handling your request.");
            }
        }
    }
}
