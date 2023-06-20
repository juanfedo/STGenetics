using Application.DTO;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.Enums.FilterEnums;

namespace STGenetics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        readonly IAnimalService _animalService;

        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        /// <summary>
        /// Create a new animal
        /// </summary>
        /// <param name="animal">The details of the new animal.</param>
        /// <returns>The id for the new animal</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAnimal(Animal animal, CancellationToken cancellationToken)
        {
            try
            {
                var newAnimalId = await _animalService.CreateAnimalAsync(animal, cancellationToken);
                return Ok(newAnimalId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Update a existing animal
        /// </summary>
        /// <param name="animal">The updated details of the animal</param>
        /// <returns>200 if was a successful operation</returns>
        [HttpPut()]
        [Authorize]
        public async Task<IActionResult> UpdateAnimal(Animal animal, CancellationToken cancellationToken)
        {
            try
            {
                var resultOperation = await _animalService.UpdateAnimalAsync(animal, cancellationToken);
                if (!resultOperation)
                {
                    return StatusCode(404, "Animal doesn´t exists in database");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Delete a existing animal
        /// </summary>
        /// <param name="animalId">Id of the animal</param>
        /// <returns>200 if was a successful operation</returns>
        [HttpDelete("{animalId}")]
        [Authorize]
        public async Task<IActionResult> DeleteAnimal(int animalId, CancellationToken cancellationToken)
        {
            try
            {
                var resultOperation = await _animalService.DeleteAnimalAsync(animalId, cancellationToken);
                if (!resultOperation)
                {
                    return StatusCode(404, "Animal doesn´t exists in database");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Filter animals by AnimalId, Name, Sex, Status
        /// </summary>
        /// <param name="filter">AnimalFilter enumerator</param>
        /// <param name="value">Any value to search</param>
        /// <returns>List of objects that satisfy the condition</returns>
        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> FilterAnimals(AnimalFilter filter, string value, CancellationToken cancellationToken)
        {
            try
            {
                var filteredAnimals = await _animalService.FilterAnimalAsync(new AnimalFilterRequest()
                {
                    Filter = filter,
                    Value = value
                }, cancellationToken);
                return Ok(filteredAnimals);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
