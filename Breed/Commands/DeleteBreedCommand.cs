using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SimpleAPI.Breed.Commands
{
    public record DeleteBreedCommand(int Id): IRequest;

    public class DeleteBreedHandler : IRequestHandler<DeleteBreedCommand>
    {
        private readonly IConfiguration _configuration;

        public DeleteBreedHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Handle(DeleteBreedCommand request, CancellationToken cancellationToken)
        {
            string? connectionString = _configuration.GetConnectionString("Default");

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync("DELETE [Breed] WHERE [Id] = @Id;", request);
            }
        }
    }
}
