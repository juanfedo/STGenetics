using Domain.Models;

namespace Infrastructure.Repositories
{
    public interface IAnimalRepository
    {
        Task<bool> DeleteAnimalAsync(int animalId, CancellationToken cancellationToken);
        Task<int> CreateAnimalAsync(Animal animal, CancellationToken cancellationToken);
        Task<bool> UpdateAnimalAsync(Animal animal, CancellationToken cancellationToken);
        Task<float> PriceByAnimalAsync(int animalId, CancellationToken cancellationToken);
        Task<List<Animal>> FilterAnimalAsync(string column, string? value, CancellationToken cancellationToken);
    }
}