using System;
using System.Text;
using Figures.API.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        private IFigureRepository _figureRepository;
        private const string StatusCode500Message = "A problem has happened while handling your request.";

        public FiguresController(ILogger<FiguresController> logger, IFigureRepository figureRepository)
        {
            _logger = logger;
            _figureRepository = figureRepository;
        }

        [HttpGet()]
        public IActionResult GetFigures()
        {
            var figureEntities = _figureRepository.GetFigures();
            var result = AutoMapper.Mapper.Map<IEnumerable<FigureDto>>(figureEntities);

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetFigure")]
        public IActionResult GetFigure(int id)
        {
            try
            {
                var figureToReturn = _figureRepository.GetFigure(id);

                if (figureToReturn == null)
                {
                    _logger.LogInformation($"Figure with id {id} wasn't found.");
                    return NotFound();
                }

                var result = AutoMapper.Mapper.Map<FigureDto>(figureToReturn);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"Exception occurred while getting figure with id {id}", e);
                return StatusCode(500, StatusCode500Message);
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

                var mappedNewFigure = AutoMapper.Mapper.Map<Entities.Figure>(newFigure);
                _figureRepository.AddFigure(mappedNewFigure);

                TryValidateModel(mappedNewFigure);

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"New figure to insert is invalid: {ModelState.ValidationState}");
                    return BadRequest();
                }

                if (!_figureRepository.Save())
                {
                    return StatusCode(500, StatusCode500Message);
                }

                return CreatedAtRoute(
                    routeName: "GetFigure",
                    routeValues: new { id = mappedNewFigure.Id },
                    value: mappedNewFigure);
            }
            catch (Exception e)
            {
                _logger.LogCritical("Exception occurred while inserting new figure.", e);
                return StatusCode(500, StatusCode500Message);
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
                figureFromStore.FullName = FieldProcessor.CalculateFullName(updatedFigure);

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogCritical("Exception occurred while updating a figure.", e);
                return StatusCode(500, StatusCode500Message);
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
                    FullName = FieldProcessor.CalculateFullName(figureFromStore)
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
                figureFromStore.FullName = FieldProcessor.CalculateFullName(figureToPatch);

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogCritical("Exception occurred while patching a figure.", e);
                return StatusCode(500, StatusCode500Message);
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
                return StatusCode(500, StatusCode500Message);
            }
        }
    }
}
