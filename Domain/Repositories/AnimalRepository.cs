using Dapper;
using Domain.Models;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Domain.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly string? _connectionString;

        public AnimalRepository(IOptions<AppSettingsModel> settings)
        {
            this._connectionString = settings.Value.DbConnection;
        }

        public async Task<int> CreateAnimalAsync(Animal animal)
        {
            using (SqlConnection con = new(_connectionString))
            {
                var param = SetParameters(animal);
                param.Add("@retID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var res = await con.ExecuteAsync("SP_InsertAnimal", param, commandType: CommandType.StoredProcedure);
                var createID = param.Get<int>("@retID");

                return createID;
            }
        }

        public async Task<bool> UpdateAnimalAsync(Animal animal)
        {
            using (SqlConnection con = new(_connectionString))
            {
                if (!AnimalExits(animal.AnimalId, con))
                {
                    return false;
                }

                var param = SetParameters(animal);

                await con.QueryAsync("SP_UpdateAnimal", param, commandType: CommandType.StoredProcedure);

                return true;
            }
        }

        private static bool AnimalExits(int animalId, SqlConnection con)
        {
            var animal = con.QueryFirstOrDefault<Animal>("SELECT * FROM Animal WHERE AnimalId = @animalId", param: new { animalId });

            return animal != null;
        }

        public async Task<bool> DeleteAnimalAsync(int animalId)
        {
            using (SqlConnection con = new(_connectionString))
            {
                if (!AnimalExits(animalId, con))
                {
                    return false;
                }

                await con.QueryAsync<Animal>("DELETE FROM Animal WHERE AnimalId = @animalId", param: new { animalId });

                return true;
            }
        }

        public async Task<float> PriceByAnimalAsync(int animalId)
        {
            using (SqlConnection con = new(_connectionString))
            {
                var sql = "SELECT Price FROM [Animal] WHERE AnimalId = @animalId";

                var priceByAnimal = await con.ExecuteScalarAsync<float>(sql, param: new { animalId });

                return priceByAnimal;
            }
        }

        public async Task<List<Animal>> FilterAnimalAsync(string column, string? value)
        {
            using (SqlConnection con = new(_connectionString))
            {
                var sql = $"SELECT * FROM [Animal] WHERE {column} = @Value";

                var animals = await con.QueryAsync<Animal>(sql, param: new { Value = value?.Trim().ToLower() });

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
