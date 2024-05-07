using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SimpleAPI.Breed.Queries
{
    public record GetBreedQuery(int Id) : IRequest<Breed>;

    public class GetBreedHandler : IRequestHandler<GetBreedQuery, Breed>
    {
        private readonly IConfiguration _configuration;

        public GetBreedHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Breed> Handle(GetBreedQuery request, CancellationToken cancellationToken)
        {
            string? connectionString = _configuration.GetConnectionString("Default");

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var breed = await connection.QueryFirstOrDefaultAsync<Breed>("SELECT [Id], [Name], [CountryOrigin] FROM [Breed] WHERE [Id] = @Id;", request);

                return breed;
            }

        }
    }

}
