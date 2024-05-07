using MediatR;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using Azure.Core;

namespace SimpleAPI.Dog.Commands
{
    public record CreateDogCommand(string Name, DateTime BirthDate, int BreedId) : IRequest<int>;

    public class CreateDogHandler : IRequestHandler<CreateDogCommand, int>
    {
        private readonly IConfiguration _configuration;

        public CreateDogHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> Handle(CreateDogCommand request, CancellationToken cancellationToken)
        {
            string? connectionString = _configuration.GetConnectionString("Default");

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.QueryFirstOrDefaultAsync<SimpleAPI.Breed.Breed>("SELECT [Id], [Name], [CountryOrigin] FROM [Breed] WHERE [Id] = @Id;", new { Id = request.BreedId});
                
                var insertedId = await connection.QuerySingleOrDefaultAsync<int>(@"
                    INSERT INTO [Dog] ([Name], [BirthDate], [BreedId])
                    OUTPUT INSERTED.Id                    
                    VALUES (@Name, @BirthDate, @BreedId);", request);

                return insertedId;
            }
        }
    }
}
