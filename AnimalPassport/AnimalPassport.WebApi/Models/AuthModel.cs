using System.ComponentModel.DataAnnotations;

namespace AnimalPassport.WebApi.Models
{
    public class AuthModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}