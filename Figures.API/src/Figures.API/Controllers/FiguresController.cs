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

                if (!_figureRepository.DoesFigureExist(id))
                {
                    _logger.LogInformation($"Figure with id {id} wasn't found.");
                    return NotFound();
                }

                var result = AutoMapper.Mapper.Map<FigureDto>(_figureRepository.GetFigure(id));
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

                if (!_figureRepository.DoesFigureExist(id))
                {
                    _logger.LogInformation($"Figure to update with id {id} wasn't found.");
                    return NotFound();
                }

                var figureEntity = _figureRepository.GetFigure(id);
                AutoMapper.Mapper.Map(updatedFigure, figureEntity);

                if (!_figureRepository.Save())
                {
                    return StatusCode(500, StatusCode500Message);
                }

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

                if (!_figureRepository.DoesFigureExist(id))
                {
                    _logger.LogInformation($"Figure to patch with id {id} wasn't found.");
                    return NotFound();
                }

                var figureEntity = _figureRepository.GetFigure(id);
                var figureToPatch = AutoMapper.Mapper.Map<FigureForUpdateDto>(figureEntity);

                patchDoc.ApplyTo(figureToPatch, ModelState);

                TryValidateModel(figureToPatch);
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Figure to patch is invalid: {ModelState.ValidationState}");
                    return BadRequest();
                }

                AutoMapper.Mapper.Map(figureToPatch, figureEntity);

                if (!_figureRepository.Save())
                {
                    return StatusCode(500, StatusCode500Message);
                }

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
                if (!_figureRepository.DoesFigureExist(id))
                {
                    _logger.LogInformation($"Figure to delete with id {id} wasn't found.");
                    return NotFound();
                }

                var figureToDelete = _figureRepository.GetFigure(id);
                _figureRepository.DeleteFigure(figureToDelete);

                if (!_figureRepository.Save())
                {
                    return StatusCode(500, StatusCode500Message);
                }

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
