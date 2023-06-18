using Domain.Models;

namespace Domain.Repositories
{
    public interface IAnimalRepository
    {
        Task<bool> DeleteAnimalAsync(int animalId);
        Task<int> CreateAnimalAsync(Animal animal);
        Task<bool> UpdateAnimalAsync(Animal animal);
    }
}