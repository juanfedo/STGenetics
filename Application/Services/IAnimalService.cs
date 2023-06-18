using Domain.Models;

namespace Application.Services
{
    public interface IAnimalService
    {
        Task<int> CreateAnimalAsync(Animal animal);
        Task<bool> UpdateAnimalAsync(Animal animal);
        Task<bool> DeleteAnimalAsync(int animalId);
        int FilterAnimal(Animal animal);
    }
}