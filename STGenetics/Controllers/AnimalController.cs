using Application.DTO;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Http;
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
        

        [HttpPost]
        public async Task<IActionResult> CreateAnimal(Animal animal)
        {
            try
            {
                var newAnimalId = await _animalService.CreateAnimalAsync(animal);
                return Ok(newAnimalId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateAnimal(Animal animal)
        {
            try
            {
                var resultOperation = await _animalService.UpdateAnimalAsync(animal);
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


        [HttpDelete("{animalId}")]
        public async Task<IActionResult> DeleteAnimal(int animalId)
        {
            try
            {
                var resultOperation = await _animalService.DeleteAnimalAsync(animalId);
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

        [HttpGet()]
        public async Task<IActionResult> FilterAnimals(AnimalFilter filter, string value)
        {
            try
            {
                var filteredAnimals = await _animalService.FilterAnimalAsync(new AnimalFilterRequest()
                {
                    Filter = filter,
                    Value = value
                });
                return Ok(filteredAnimals);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
