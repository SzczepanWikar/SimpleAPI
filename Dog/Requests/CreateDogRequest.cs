using System.ComponentModel.DataAnnotations;

namespace SimpleAPI.Dog.Requests
{
    public record CreateDogRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public int BreedId { get; set; }
    }
}
