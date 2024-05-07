using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SimpleAPI.Breed.Queries
{
    public record GetBreedsQuery : IRequest<IEnumerable<Breed>>;
    public class GetBreedsHandler: IRequestHandler<GetBreedsQuery, IEnumerable<Breed>>
    {
        private readonly IConfiguration _configuration;

        public GetBreedsHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Breed>> Handle(GetBreedsQuery request, CancellationToken cancellationToken)
        {
            string? connectionString = _configuration.GetConnectionString("Default");
            
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var breeds = await connection.QueryAsync<Breed>("SELECT [Id], [Name], [CountryOrigin] FROM [Breed];");

                return breeds;
            }

        }
    }
}
