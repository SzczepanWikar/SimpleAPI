using System.ComponentModel.DataAnnotations;

namespace SimpleAPI.Breed.Requests
{
    public record CreateBreedRequest
    {
        [Required]
        public string Name { get; set; }
        public string? CountryOrigin { get; set; }
    }
}
