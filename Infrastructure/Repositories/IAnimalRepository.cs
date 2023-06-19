using Domain.Models;

namespace Infrastructure.Repositories
{
    public interface IAnimalRepository
    {
        Task<bool> DeleteAnimalAsync(int animalId);
        Task<int> CreateAnimalAsync(Animal animal);
        Task<bool> UpdateAnimalAsync(Animal animal);
        Task<float> PriceByAnimalAsync(int animalId);
        Task<List<Animal>> FilterAnimalAsync(string column, string? value);
    }
}