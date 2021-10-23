using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}