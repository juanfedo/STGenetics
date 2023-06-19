using Application.DTO;
using Domain.Models;

namespace Application.Services
{
    public interface IAnimalService
    {
        Task<int> CreateAnimalAsync(Animal animal);
        Task<bool> UpdateAnimalAsync(Animal animal);
        Task<bool> DeleteAnimalAsync(int animalId);
        Task<List<Animal>> FilterAnimalAsync(AnimalFilterRequest request);
        Task<float> PriceByAnimalAsync(int animalId);
    }
}