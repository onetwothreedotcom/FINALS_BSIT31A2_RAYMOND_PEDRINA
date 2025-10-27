using System.ComponentModel.DataAnnotations;

namespace Pet_Adoption_Forum.Models  
{
    public class Pet
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        [Required]
        public string Breed { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public bool IsAdopted { get; set; } = false;
    }
}
