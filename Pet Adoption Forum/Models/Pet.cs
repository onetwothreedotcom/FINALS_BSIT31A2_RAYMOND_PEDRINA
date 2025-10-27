using System.ComponentModel.DataAnnotations;

namespace PetAdoption.Models
{
    public class Pet
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Age { get; set; }

        [Required]
        public string Breed { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public bool IsAdopted { get; set; } = false;
    }
}
