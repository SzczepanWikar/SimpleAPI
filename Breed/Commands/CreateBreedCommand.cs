using MediatR;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace SimpleAPI.Breed.Commands
{
    public record CreateBreedCommand(string Name, string? CountryOrigin) : IRequest<int>;

    public class CreateBreedHandler : IRequestHandler<CreateBreedCommand, int>
    {
        private readonly IConfiguration _configuration;

        public CreateBreedHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> Handle(CreateBreedCommand request, CancellationToken cancellationToken)
        {
            string? connectionString = _configuration.GetConnectionString("Default");

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var insertedId = await connection.QuerySingleOrDefaultAsync<int>(@"
                    INSERT INTO [Breed] ([Name], [CountryOrigin])
                    OUTPUT INSERTED.Id                    
                    VALUES (@Name, @CountryOrigin);", request);

                return insertedId;
            }

        }
    }
}
