using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeShare.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Role { get; set; }

        public string Bio { get; set; }
    }
}

