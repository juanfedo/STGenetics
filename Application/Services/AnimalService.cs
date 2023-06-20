using Application.DTO;
using Application.Validation;
using Domain.Models;
using Infrastructure.Repositories;
using static Application.Enums.FilterEnums;

namespace Application.Services
{
    public class AnimalService : IAnimalService
    {
        readonly IAnimalRepository _animalRepository;

        public AnimalService(IAnimalRepository animalRepository) =>
            this._animalRepository = animalRepository;

        public async Task<int> CreateAnimalAsync(Animal animal, CancellationToken cancellationToken) =>
            await _animalRepository.CreateAnimalAsync(animal, cancellationToken);

        public async Task<bool> DeleteAnimalAsync(int animalId, CancellationToken cancellationToken) =>
            await _animalRepository.DeleteAnimalAsync(animalId, cancellationToken);

        public async Task<float> PriceByAnimalAsync(int animalId, CancellationToken cancellationToken) =>
            await _animalRepository.PriceByAnimalAsync(animalId, cancellationToken);

        public async Task<List<Animal>> FilterAnimalAsync(AnimalFilterRequest request, CancellationToken cancellationToken) =>
            request.Filter switch
            {
                AnimalFilter.AnimalId => await _animalRepository.FilterAnimalAsync(Constants.Constants.ANIMALID, request.Value, cancellationToken),
                AnimalFilter.Name => await _animalRepository.FilterAnimalAsync(Constants.Constants.NAME, request.Value, cancellationToken),
                AnimalFilter.Sex => ValidateInput.ValidateSex(request.Value) ? await _animalRepository.FilterAnimalAsync(Constants.Constants.SEX, request.Value, cancellationToken) : throw new Exception("Value should by: male, female"),
                AnimalFilter.Status => ValidateInput.ValidateStatus(request.Value) ? await _animalRepository.FilterAnimalAsync(Constants.Constants.STATUS, request.Value, cancellationToken)  : throw new Exception("Value should by: active, inactive"),
                _ => throw new NotImplementedException(),
            };

        public async Task<bool> UpdateAnimalAsync(Animal animal, CancellationToken cancellationToken) =>
             await _animalRepository.UpdateAnimalAsync(animal, cancellationToken);

    }
}
