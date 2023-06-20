using Dapper;
using Domain.Models;
using Infrastructure.Entities;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly string? _connectionString;

        public AnimalRepository(IOptions<AppSettingsModel> settings)
        {
            this._connectionString = settings.Value.DbConnection;
        }

        public async Task<int> CreateAnimalAsync(Animal animal, CancellationToken cancellationToken)
        {
            using (SqlConnection con = new(_connectionString))
            {
                var param = SetParameters(animal);
                param.Add("@retID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var res = await con.ExecuteAsync(new CommandDefinition("SP_InsertAnimal", param, commandType: CommandType.StoredProcedure, cancellationToken: cancellationToken));
                var createID = param.Get<int>("@retID");

                return createID;
            }
        }

        public async Task<bool> UpdateAnimalAsync(Animal animal, CancellationToken cancellationToken)
        {
            using (SqlConnection con = new(_connectionString))
            {
                if (!await AnimalExitsAsync(animal.AnimalId, con, cancellationToken))
                {
                    return false;
                }

                var param = SetParameters(animal);
                param.Add("@AnimalId", animal.AnimalId);

                await con.QueryAsync(new CommandDefinition ("SP_UpdateAnimal", param, commandType: CommandType.StoredProcedure, cancellationToken : cancellationToken));

                return true;
            }
        }

        private async Task<bool> AnimalExitsAsync(int animalId, SqlConnection con, CancellationToken cancellationToken)
        {
            var animal = await con.QueryAsync<Animal>(new CommandDefinition("SELECT * FROM Animal WHERE AnimalId = @animalId", parameters: new { animalId }, cancellationToken: cancellationToken));

            return animal != null;
        }

        public async Task<bool> DeleteAnimalAsync(int animalId, CancellationToken cancellationToken)
        {
            using (SqlConnection con = new(_connectionString))
            {
                if (!await AnimalExitsAsync(animalId, con, cancellationToken))
                {
                    return false;
                }

                await con.QueryAsync<Animal>(new CommandDefinition("DELETE FROM Animal WHERE AnimalId = @animalId", parameters: new { animalId }, cancellationToken: cancellationToken));

                return true;
            }
        }

        public async Task<float> PriceByAnimalAsync(int animalId, CancellationToken cancellationToken)
        {
            using (SqlConnection con = new(_connectionString))
            {
                var sql = "SELECT Price FROM [Animal] WHERE AnimalId = @animalId";

                var priceByAnimal = await con.ExecuteScalarAsync<float>(new CommandDefinition(sql, parameters: new { animalId }, cancellationToken: cancellationToken));

                return priceByAnimal;
            }
        }

        public async Task<List<Animal>> FilterAnimalAsync(string column, string? value, CancellationToken cancellationToken)
        {
            using (SqlConnection con = new(_connectionString))
            {
                var sql = $"SELECT * FROM [Animal] WHERE {column} = @Value";

                var animals = await con.QueryAsync<Animal>(new CommandDefinition(sql, parameters: new { Value = value?.Trim().ToLower() }, cancellationToken: cancellationToken));

                return animals.ToList();
            }
        }

        private static DynamicParameters SetParameters(Animal animal)
        {
            var param = new DynamicParameters();
            param.Add("@Name", animal.Name);
            param.Add("@Breed", animal.Breed);
            param.Add("@BirthDate", animal.BirthDate);
            param.Add("@Sex", animal.Sex);
            param.Add("@Price", animal.Price);
            param.Add("@Status", animal.Status);

            return param;
        }
    }
}
