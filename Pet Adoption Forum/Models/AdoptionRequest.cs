using Pet_Adoption_Forum.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Pet_Adoption_Forum.Models  
{
    public class AdoptionRequest
    {
        public int Id { get; set; }

        [Required]
        public int PetId { get; set; }

        public Pet Pet { get; set; } = null!;

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        public bool IsSelected { get; set; } = false; 

        public string Status { get; set; } = "Pending";  

        public DateTime RequestDate { get; set; } = DateTime.Now;

        public string PetName { get; set; } = string.Empty; 
    }
}
