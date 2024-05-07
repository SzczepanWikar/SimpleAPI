using MediatR;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace SimpleAPI.Dog.Queries
{
    public record GetDogQuery(int Id) : IRequest<Dog>;

    public class GetDogHandler : IRequestHandler<GetDogQuery, Dog>
    {
        private readonly IConfiguration _configuration;

        public GetDogHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Dog> Handle(GetDogQuery request, CancellationToken cancellationToken)
        {
            string? connectionString = _configuration.GetConnectionString("Default");

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var dog = await connection.QueryAsync<Dog, Breed.Breed, Dog>(@"
                    SELECT  [Dog].[Id], [Dog].[Name], [Dog].[BirthDate], [Dog].[BreedId], 
                            [Breed].[Id], [Breed].[Name], [Breed].[CountryOrigin]
                    FROM [Dog] 
                    LEFT JOIN [Breed] on [Breed].[Id] = [Dog].[BreedId]
                    WHERE [Dog].[Id] = @Id;", (dog, breed) => { dog.Breed = breed; return dog; }, request,splitOn: "Id");

                return dog.FirstOrDefault();
            }

        }
    }
}
