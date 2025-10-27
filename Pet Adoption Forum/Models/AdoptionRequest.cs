using System.ComponentModel.DataAnnotations;

namespace PetAdoption.Models
{
    public class AdoptionRequest
    {
        public int Id { get; set; }

        [Required]
        public int PetId { get; set; }
        public Pet Pet { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int Age { get; set; }

        public bool IsSelected { get; set; } = false;
    }
}
