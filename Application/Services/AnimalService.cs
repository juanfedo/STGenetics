using Domain.Models;
using Domain.Repositories;

namespace Application.Services
{
    public class AnimalService : IAnimalService
    {
        readonly IAnimalRepository _animalRepository;

        public AnimalService(IAnimalRepository animalRepository)
        {
            this._animalRepository = animalRepository;
        }

        public async Task<int> CreateAnimalAsync(Animal animal)
        {
            return await _animalRepository.CreateAnimalAsync(animal);
        }

        public async Task<bool> DeleteAnimalAsync(int animalId)
        {
            return await _animalRepository.DeleteAnimalAsync(animalId);
        }

        public int FilterAnimal(Animal animal)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAnimalAsync(Animal animal)
        {
            return await _animalRepository.UpdateAnimalAsync(animal);
        }
    }
}
