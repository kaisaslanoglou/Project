using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HandmadeShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Fullname { get; set; }

        public string? StreetAddress { get; set; }
        public string? City { get; set; }

    }
}
