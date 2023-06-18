using Dapper;
using Domain.Models;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

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
                con.Open();

                SqlTransaction sqltrans = con.BeginTransaction();

                var param = new DynamicParameters();
                param.Add("@Name", animal.Name);
                param.Add("@Breed", animal.Breed);
                param.Add("@BirthDate", animal.BirthDate);
                param.Add("@Sex", animal.Sex);
                param.Add("@Price", animal.Price);
                param.Add("@Status", animal.Status);
                param.Add("@retID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var res = await con.ExecuteAsync("SP_InsertAnimal", param, sqltrans, 0, CommandType.StoredProcedure);
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

                con.Open();

                SqlTransaction sqltrans = con.BeginTransaction();

                var param = new DynamicParameters();
                param.Add("@Name", animal.Name);
                param.Add("@Breed", animal.Breed);
                param.Add("@BirthDate", animal.BirthDate);
                param.Add("@Sex", animal.Sex);
                param.Add("@Price", animal.Price);
                param.Add("@Status", animal.Status);
                param.Add("@retID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                _ = await con.ExecuteAsync("SP_UpdateAnimal", param, sqltrans, 0, CommandType.StoredProcedure);

                return true;
            }
        }

        private static bool AnimalExits(int animalId, SqlConnection con)
        {
            con.Open();

            var animal = con.QueryFirstOrDefault<Animal>("SELECT * FROM Animal WHERE AnimalId = @animalId", param: new { animalId });

            con.Close();

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

                con.Open();

                _ = await con.QueryAsync<Animal>("DELETE FROM Animal WHERE AnimalId = @animalId", param: new { animalId });

                return true;
            }
        }

    }

}
