using Application.DTO;
using Domain.Models;

namespace Application.Services
{
    public interface IAnimalService
    {
        Task<int> CreateAnimalAsync(Animal animal, CancellationToken cancellationToken);
        Task<bool> UpdateAnimalAsync(Animal animal, CancellationToken cancellationToken);
        Task<bool> DeleteAnimalAsync(int animalId, CancellationToken cancellationToken);
        Task<List<Animal>> FilterAnimalAsync(AnimalFilterRequest request, CancellationToken cancellationToken);
        Task<float> PriceByAnimalAsync(int animalId, CancellationToken cancellationToken);
    }
}