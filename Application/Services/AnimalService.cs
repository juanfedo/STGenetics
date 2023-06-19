using Application.DTO;
using Application.Validation;
using Domain.Models;
using Domain.Repositories;
using static Application.Enums.FilterEnums;

namespace Application.Services
{
    public class AnimalService : IAnimalService
    {
        readonly IAnimalRepository _animalRepository;

        public AnimalService(IAnimalRepository animalRepository) =>
            this._animalRepository = animalRepository;

        public async Task<int> CreateAnimalAsync(Animal animal) =>
            await _animalRepository.CreateAnimalAsync(animal);

        public async Task<bool> DeleteAnimalAsync(int animalId) =>
            await _animalRepository.DeleteAnimalAsync(animalId);

        public async Task<float> PriceByAnimalAsync(int animalId) =>
            await _animalRepository.PriceByAnimalAsync(animalId);

        public async Task<List<Animal>> FilterAnimalAsync(AnimalFilterRequest request) =>
            request.Filter switch
            {
                AnimalFilter.AnimalId => await _animalRepository.FilterAnimalAsync(Constants.Constants.ANIMALID, request.Value),
                AnimalFilter.Name => await _animalRepository.FilterAnimalAsync(Constants.Constants.NAME, request.Value),
                AnimalFilter.Sex => ValidateInput.ValidateSex(request.Value) ? await _animalRepository.FilterAnimalAsync(Constants.Constants.SEX, request.Value) : throw new Exception("Value should by: male, female"),
                AnimalFilter.Status => ValidateInput.ValidateStatus(request.Value) ? await _animalRepository.FilterAnimalAsync(Constants.Constants.STATUS, request.Value)  : throw new Exception("Value should by: active, inactive"),
                _ => throw new NotImplementedException(),
            };

        public async Task<bool> UpdateAnimalAsync(Animal animal) =>
             await _animalRepository.UpdateAnimalAsync(animal);

    }
}
